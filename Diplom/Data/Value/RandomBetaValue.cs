using Diplom.Data.Exeption;
using Diplom.Data.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Value
{
    class RandomBetaValue : AbstractValue
    {
        private double alpha;

        private double beta;

        private RandomBasicValue basicRandom;

        private List<Double> forIntegerGenerate = null;

        private bool isIntegerType;

        public static readonly String ALPHA = "Alpha";

        public static readonly String BETA = "Beta";

        public RandomBetaValue()
        {
            basicRandom = new RandomBasicValue();
        }

        public override double nextValue()
        {
            if (isIntegerType)
                return nextInteger();
            return nextFractional();
        }

        private double nextInteger()
        {
            forIntegerGenerate.Clear();
            Double x = 0;
            for (int i = 0; i < alpha + beta + 1; i++)
            {
                x = basicRandom.nextValue();
                int j = 0;
                while (j < forIntegerGenerate.Count && x > forIntegerGenerate[j])
                {
                    j++;
                }
                if (j < forIntegerGenerate.Count)
                    forIntegerGenerate.Insert(j, x);
                else
                    forIntegerGenerate.Add(x);
            }
            return forIntegerGenerate[(int)alpha];
        }

        private double nextFractional()
        {
            return 0;
        }

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(ALPHA, alpha);
            state.Add(BETA, beta);
            return state;
        }

        public override void restore(JObject state)
        {
            alpha = (Double)state.GetValue(ALPHA);
            beta = (Double)state.GetValue(BETA);
        }

        public override void init()
        {
            if (alpha % 1 < Utils.EPSILON && beta % 1 < Utils.EPSILON)
                isIntegerType = true;
            else
            {
                isIntegerType = false;
                //TODO: доделать
                throw new CreateModelException("Fractional not implemented");               
            }
        }

        public override void validate()
        {
            if (alpha <= 0)
                throw new CreateModelException("alpha <=0");
            if (beta <= 0)
                throw new CreateModelException("beta <=0");
        }
    }
}
