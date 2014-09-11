using Diplom.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Business
{
    /// <summary>
    /// Абстракция бизнес-процесса, который протекает в предметной области
    /// </summary>
    abstract class AbstractBusinessProcess : AbstractStorable
    {
        public static String BUSINESS_PROCESS_NAME = "Business_process_name";

        protected String businessProcessName = "No name";

        /// <summary>
        /// Получить следующее бизнес-событие процесса
        /// </summary>
        /// <returns></returns>
        public abstract AbstractBusinessEvent nextBusinessEvent();

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(BUSINESS_PROCESS_NAME, businessProcessName);
            return state;
        }

         public override void restore(JObject state)
        {
            businessProcessName = (String)state.GetValue(BUSINESS_PROCESS_NAME);
        }
    }
}
