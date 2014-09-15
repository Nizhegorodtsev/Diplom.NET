using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Diplom.Data.Exeption;

namespace Diplom.Data.Value
{
    /// <summary>
    /// Нормально распределенная случайная величина с параметрами "альфа" и "сигма"
    /// </summary>
    class RandomNormalValue : AbstractValue
    {


        private RandomBasicValue brv1;
        private RandomBasicValue brv2;

        private double alpha = 0;
        private double sigma = 1;

        public static readonly String ALPHA = "Alpha";
        public static readonly String SIGMA = "Sigma";

        public RandomNormalValue()
        {
            brv1 = new RandomBasicValue();
            brv2 = new RandomBasicValue();
        }

        private double nextGaus()
        {
            return System.Math.Sqrt(-2 * Math.Log(1 - brv1.nextValue())) * Math.Cos(2 * Math.PI * brv2.nextValue());
        }


        public override double nextValue()
        {
            return alpha + nextGaus() * sigma;
        }

        public override void restore(JObject state)
        {
            alpha = (Double)state.GetValue(ALPHA);
            sigma = (Double)state.GetValue(SIGMA);
        }

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(ALPHA, alpha);
            state.Add(SIGMA, sigma);
            return state;
        }

        public override void init()
        {
        }

        public override void validate()
        {
            if (sigma < 0)
                throw new CreateModelException(getClassName() + ": sigma < 0");
        }
    }
}
