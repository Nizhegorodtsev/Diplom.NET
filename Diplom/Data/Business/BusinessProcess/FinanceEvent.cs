using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Business.BusinessProcess
{
    class FinanceEvent : AbstractBusinessEvent
    {
        /// <summary>
        /// Величина, на которую изменятеся капитал
        /// </summary>
        private double amount;

        public FinanceEvent()
        {
        }

        public double getAmount()
        {
            return amount;
        }

        public void setAmount(double amount)
        {
            this.amount = amount;
        }
    }
}
