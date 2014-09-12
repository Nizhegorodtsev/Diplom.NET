using Diplom.Data.Exeption;
using Diplom.Data.Random;
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
    /// Марковский модулированный пуассоновский поток
    /// </summary>
    class MMPP : AbstractProcess
    {
        /// <summary>
        /// Текущее состояние
        /// </summary>
        protected int currentState = 0;

        /// <summary>
        /// Число различных состояний
        /// </summary>
        protected int countOfStates = 1;

        /// <summary>
        /// Фиксируется время пребывания в каждом состоянии
        /// </summary>
        protected double[] timeInStateVector = null;

        /// <summary>
        /// Вектор частот наступления событий в каждом состоянии
        /// </summary>
        protected double[] occurrenceFrequencyVector = null;

        /// <summary>
        /// Матрица вероятностей переходов переходов
        /// </summary>
        protected double[][] changeStateMatrix = null;

        /// <summary>
        /// Случайная величина отвечает за генерацию состояния, в которое перейдет поток при окончании времени пребывания в текущем состоянии
        /// </summary>
        protected RandomBasicValue stateSelector = null;

        /// <summary>
        /// Вектор случайных величин, отвечающих за герерацию времени пребывания в состояниях. Каждая из случайных величин отвечает за генерацию времени пребывания в соответствующем состоянии
        /// </summary>
        protected List<RandomExponentialValue> timeInStateGenerator = null;

        /// <summary>
        /// Вектор случайных величин, отвечающих за генерацию времени наступления события. Каждая случайная величина генерирует время время наступления событий для соответствующего состояния
        /// </summary>
        protected List<RandomExponentialValue> eventTimeGenerator = null;

        /// <summary>
        /// Текущее время
        /// </summary>
        protected double currentTime = 0;

        /// <summary>
        /// Время, до которого поток будет пребывать в текущем состоянии
        /// </summary>
        protected double timeInCurrentState = 0;

        public static readonly String COUNT_OF_STATES = "Count_of_states";

        public static readonly String CURRENT_STATE = "Current_state";

        public static readonly String INF_MARTIX = "Inf_matrix";

        public static readonly String TIME_IN_STATE_VECTOR = "Time_in_state";

        public static String LAMBDA_VECTOR = "Lambda";

        public MMPP()
        {
            stateSelector = new RandomBasicValue();
            eventTimeGenerator = new List<RandomExponentialValue>();
            timeInStateGenerator = new List<RandomExponentialValue>();
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

            //  Иначе происходит переход в новое состояние
            selectState();
            /// Рекурсивно вызывается метод nextValue(). Предполагается, что, в подавляющем большенстве случаев, выход из рекурсии 
            ///  будет происходить не более чем за 1 дополнительный вызов метода. Для этого необходимо, чтобы события наступали интенсивнее,
            ///  чем сменяются состояния потока, иначе рекурсия может быть неконтролируемой Х_Х
            return nextValue();
        }

        /// <summary>
        /// Переход в новое состояние потока
        /// </summary>
        protected virtual void selectState()
        {
            //текущее время полагается равным времени окончания пребывания потока в текущем состоянии 
            currentTime = timeInCurrentState;
            currentState = -1;
            double[] pos = changeStateMatrix[currentState];
            double value = stateSelector.nextValue();
            int i = 0;
            while (i < pos.Length && value > pos[i])
            {
                i++;
            }
            if (i < pos.Length)
                currentState = i;
            else
                currentState = countOfStates - 1;

            timeInCurrentState = timeInStateGenerator[currentState].nextValue();
        }

        public override void restore(JObject state)
        {
            countOfStates = (int)state.GetValue(COUNT_OF_STATES);
            timeInStateVector = JsonUtils.restoreVector((JArray)state.GetValue(TIME_IN_STATE_VECTOR));
            occurrenceFrequencyVector = JsonUtils.restoreVector((JArray)state.GetValue(LAMBDA_VECTOR));
            changeStateMatrix = JsonUtils.restoreMatrix((JArray)state.GetValue(INF_MARTIX));
        }

        public override JObject store()
        {
            JObject obj = base.store();
            return obj;
        }

        public override void init()
        {
            for (int i = 0; i < countOfStates; i++)
            {
                eventTimeGenerator.Add(new RandomExponentialValue(occurrenceFrequencyVector[i]));
                timeInStateGenerator.Add(new RandomExponentialValue(timeInStateVector[i]));
            }
            selectState();
        }

        public override void validate()
        {
            if (occurrenceFrequencyVector.Length != countOfStates)
                throw new CreateModelException("Длина вектора частот наступления событий должна равняться числу состояний");

            if (timeInStateVector.Length != countOfStates)
                throw new CreateModelException("Длина вектора \"продолжительности\" должна равняться числу состояний");

            if (changeStateMatrix.Length != countOfStates)
                throw new CreateModelException("Матрица переходов должна быть размерностью " + countOfStates + "x" + countOfStates);

            for (int i = 0; i < countOfStates; i++)
            {
                if (changeStateMatrix[i].Length != countOfStates)
                    throw new CreateModelException("Матрица переходов должна быть размерностью " + countOfStates + "x" + countOfStates);

                if (changeStateMatrix[i][i] >= Utils.EPSILON)
                    throw new CreateModelException("Диагональные элементы матрицы переходов должны быть равны нулю");

                double probability = 0;
                for (int j = 0; j < countOfStates; j++)
                {
                    probability += changeStateMatrix[i][j];
                    if (changeStateMatrix[i][j] > 1)
                        throw new CreateModelException("changeStateEventMatrix[" + i + "] Сумма вероятностей перехода должна быть равно 1");
                }
                if (Math.Abs(probability - 1) > Utils.EPSILON)
                    throw new CreateModelException("changeStateMatrix[" + i + "] Сумма вероятностей перехода должна быть равно 1");
            }
        }
    }
}
