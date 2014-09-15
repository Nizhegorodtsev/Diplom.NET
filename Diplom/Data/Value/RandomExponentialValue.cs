using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Diplom.Data.Exeption;
using Diplom.Data.Utilities;

namespace Diplom.Data.Value
{
    /// <summary>
    /// Экспоненциально распределенная случайная величина с параметром "лямбда"
    /// </summary>
    class RandomExponentialValue : AbstractValue
    {
        private double lambda = 1;

        private RandomBasicValue basicRandomValue = null;

        public static readonly String LAMBDA = "Lambda";

        public RandomExponentialValue()
        {
            basicRandomValue = new RandomBasicValue();
        }

        public RandomExponentialValue(double lambda)
        {
            basicRandomValue = new RandomBasicValue();
            this.lambda = lambda;
        }

        public override double nextValue()
        {
            double u;
            while ((u = basicRandomValue.nextValue()) <= Utils.EPSILON)
                ;
            return -1.0 / lambda * Math.Log(u);
        }

        public override void restore(JObject state)
        {
            lambda = (Double)state.GetValue(LAMBDA);
        }

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(LAMBDA, lambda);
            return state;
        }

        public override void init()
        {
        }

        public override void validate()
        {
            if (lambda <= Utils.EPSILON)
                throw new CreateModelException("lambda is too small");
        }

        public double getLambda()
        {
            return lambda;
        }

        public void setLambda(double lambda)
        {
            this.lambda = lambda;
        }
    }
}
