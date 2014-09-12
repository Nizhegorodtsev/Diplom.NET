using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json.Linq;
using Diplom.Data.Exeption;
using Diplom.Data.Business.BusinessProcess;
using Diplom.Data.Utilities;

namespace Diplom.Data.Business.Model
{
    class CapitalModel : AbstractModel
    {
        /// <summary>
        /// Стартовый капитал
        /// </summary>
        private double startCapital = 0;
        private double maxIncome = 0;
        private double minIncome = 0;
        private double maxPayment = 0;
        private double minPayment = 0;

        /// <summary>
        /// Список бизнес-процессов модели
        /// </summary>
        private List<AbstractBusinessProcess> businessProcessList;

        /// <summary>
        /// Текущий капитал
        /// </summary>
        private double capital = 0;

        /// <summary>
        /// История изменения капитала модели
        /// </summary>
        private List<Double> capitalHistory;


        private SortedDictionary<Double, AbstractBusinessEvent> eventMap;

        public static readonly String START_CAPITAL = "Start_capital";
        public static readonly String MAX_INCOME = "Max_income";
        public static readonly String MIN_INCOME = "Min_income";
        public static readonly String MAX_PAYMENT = "Max_payment";
        public static readonly String MIN_PAYMENT = "Min_payment";
        public static readonly String CAPITAL_STREAMS = "Capital_streams";

        public CapitalModel()
        {
            capitalHistory = new List<Double>();
            eventMap = new SortedDictionary<Double, AbstractBusinessEvent>();
            businessProcessList = new List<AbstractBusinessProcess>();
        }


        public override void init()
        {
            foreach (AbstractBusinessProcess stream in businessProcessList)
            {
                AbstractBusinessEvent evt = stream.nextBusinessEvent();
                eventMap.Add(currentTime + evt.getTime(), evt);
            }
            capital = startCapital;
            capitalHistory.Add(capital);
        }

        public override void doStep()
        {
            Double key = eventMap.Keys.First();
            AbstractBusinessEvent evt = eventMap[key];
            eventMap.Remove(key);
            if (evt.GetType() == typeof(FinanceEvent))
                financeEvent((FinanceEvent)evt);
            ///  else if (evt.GetType() == typeof(SomeEvent))
            ///  someEvent((SomeEvent)evt) итд
        }

        /// <summary>
        /// Обработка финансового события
        /// </summary>
        /// <param name="evt"></param>
        private void financeEvent(FinanceEvent evt)
        {
            capital += evt.getAmount();
            currentTime += evt.getTime();
            Console.WriteLine(capital);
            Console.Write("   " + evt.getAmount());

            if (capital < 0)
                stopRun();

            else
            {
                AbstractBusinessEvent newEvent = evt.getBusinessProcess().nextBusinessEvent();
                eventMap.Add(newEvent.getTime() + currentTime, newEvent);
                capitalHistory.Add(capital);
            }
        }

        public override void restart()
        {
            capital = startCapital;
        }

        public override void finish()
        {
            Console.WriteLine("число элементов: " + capitalHistory.Count);
        }

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(START_CAPITAL, startCapital);
            state.Add(MAX_INCOME, maxIncome);
            state.Add(MAX_PAYMENT, maxPayment);
            state.Add(MIN_INCOME, minIncome);
            state.Add(MIN_PAYMENT, minPayment);
            state.Add(CAPITAL_STREAMS, JsonUtils.storeArray(businessProcessList.Cast<AbstractStorable>().ToList()));
            return state;
        }

        public override void restore(JObject state)
        {
            try
            {
                base.restore(state);
                startCapital = (double)state.GetValue(START_CAPITAL);
                maxIncome = (double)state.GetValue(MAX_INCOME);
                maxPayment = (double)state.GetValue(MAX_PAYMENT);
                minIncome = (double)state.GetValue(MIN_INCOME);
                minPayment = (double)state.GetValue(MIN_PAYMENT);
                businessProcessList = JsonUtils.restoreArray((JArray)state.GetValue(CAPITAL_STREAMS)).Cast<AbstractBusinessProcess>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new CreateModelException(getClassName() + " restore error");
            }
        }

        public override void validate()
        {
            if (maxIncome <= minIncome)
                throw new CreateModelException("maxIncome <= minIncome");
            if (maxPayment <= minPayment)
                throw new CreateModelException("maxPayment <= minPayment");
            if (startCapital < 0)
                throw new CreateModelException("startCapital < 0");
        }
    }
}
