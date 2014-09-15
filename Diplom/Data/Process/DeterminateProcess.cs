using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Process
{
    /// <summary>
    /// Детерминированный процесс
    /// </summary>
    class DeterminateProcess : AbstractProcess
    {
        private double value = 0;

        public static readonly string VALUE = "Value";

        public override double nextValue()
        {
            return value;
        }

        public override void restore(JObject state)
        {
            value = (Double)state.GetValue(VALUE);
        }

        public override void init()
        {
        }

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(VALUE, value);
            return state;
        }

        public override void validate()
        {
        }
    }
}
