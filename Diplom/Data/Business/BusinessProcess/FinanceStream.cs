using Diplom.Data.Process;
using Diplom.Data.Random;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Business.BusinessProcess
{
    class FinanceStream : AbstractBusinessProcess
    {
        private AbstractProcess eventTimeGenerator = null;

        private AbstractRandomValue amountGenerator = null;

        private bool income = true;

        public static readonly String AMOUNT = "Amount";

        public static readonly String PROCESS = "Process";

        public static readonly String INCOME = "Income";

        public override AbstractBusinessEvent nextBusinessEvent()
        {
            FinanceEvent evt = new FinanceEvent();
            evt.setTime(eventTimeGenerator.nextValue());
            evt.setAmount(income ? amountGenerator.nextValue() : -amountGenerator.nextValue());
            evt.setBusinessProcess(this);
            return evt;
        }

        public override void restore(JObject state)
        {
            base.restore(state);
            income = (bool)state.GetValue(INCOME);
            eventTimeGenerator = (AbstractProcess)AbstractStorable.newInstance((JObject)state.GetValue(PROCESS));
            amountGenerator = (AbstractRandomValue)AbstractStorable.newInstance((JObject)state.GetValue(AMOUNT));
        }

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(INCOME, income);
            state.Add(PROCESS, eventTimeGenerator.store());
            state.Add(AMOUNT, amountGenerator.store());
            return state;
        }

        public override void validate()
        {
        }

        public override void init()
        {
        }


        public bool isIncome()
        {
            return income;
        }

        public void setIncome(bool income)
        {
            this.income = income;
        }
    }
}
