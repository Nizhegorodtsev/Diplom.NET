using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace Diplom.Data.Value
{
    /// <summary>
    /// Базовая случайная величина, распределенная на интервале [0, 1]
    /// </summary>
    class RandomBasicValue : AbstractValue
    {
        private System.Random random;

        public RandomBasicValue()
        {
            random = new System.Random();
        }

        public override double nextValue()
        {
            return random.NextDouble();
        }

        public override void init()
        {
        }

        public override void restore(JObject state)
        {
            //  base.restore(state);
        }

        public override void validate()
        {
        }
    }
}
