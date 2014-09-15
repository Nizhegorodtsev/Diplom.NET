using Diplom.Data.Exeption;
using Diplom.Data.Value;
using Diplom.Data.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Process
{
    /// <summary>
    /// МАР поток
    /// </summary>
    class MAP : MMPP
    {
        private double[][] changeStateEventMatrix;

        private RandomBasicValue changeStateEventGenerator;

        public static readonly String CHANGE_STATE_EVENT_MATRIX = "Change_state_event-matrix";

        public MAP()
            : base()
        {
            changeStateEventGenerator = new RandomBasicValue();
        }

        public override double nextValue()
        {
            double eventTime = currentTime + eventTimeGenerator[currentState].nextValue();
            ///Если время наступления события меньше времени окончания пребывания потока в текущем состоянии, то событие наступает
            if (eventTime <= timeInCurrentState)
            {
                currentTime = eventTime;
                return eventTime;
            }
            // Иначе текущее время полагается равным времени окончания пребывания потока в текущем состоянии
            currentTime = timeInCurrentState;
            // Происходит переход в новое состояние
            int previousState = currentState;
            selectState();
            // При смене состояния могло сгенерироваться событие
            if (changeStateEvent(previousState))
                return currentTime;
            /// Рекурсивно вызывается метод nextValue(). Предполагается, что, в подавляющем большенстве случаев, выход из рекурсии 
            ///  будет происходить не более чем за 1 дополнительный вызов метода. Для этого необходимо, чтобы события наступали интенсивнее,
            ///  чем сменяются состояния потока, иначе рекурсия может быть неконтролируемой Х_Х
            return nextValue();
        }

        private bool changeStateEvent(int previousState)
        {
            if (changeStateEventMatrix[previousState][currentState] < changeStateEventGenerator.nextValue())
                return true;
            return false;
        }

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(CHANGE_STATE_EVENT_MATRIX, JsonUtils.storeMatrix(changeStateEventMatrix));
            return state;
        }

        public override void restore(JObject state)
        {
            base.restore(state);
            changeStateEventMatrix = JsonUtils.restoreMatrix((JArray)state.GetValue(CHANGE_STATE_EVENT_MATRIX));
        }

        public override void validate()
        {
            base.validate();
            for (int i = 0; i < countOfStates; i++)
                for (int j = 0; j < countOfStates; j++)
                {
                    if (changeStateEventMatrix[i][j] > 1)
                        throw new CreateModelException("changeStateEventMatrix[" + i + "] Сумма вероятностей перехода должна быть равно 1");
                }
        }
    }

}
