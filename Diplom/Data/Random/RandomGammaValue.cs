using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Diplom.Data.Utilities;
using Diplom.Data.Exeption;

namespace Diplom.Data.Random
{
    class RandomGammaValue : AbstractRandomValue
    {
        private double param;

        private RandomBasicValue basicRandom;

        private RandomNormalValue normalRandom;

        private bool isIntegerType = false;

        private bool isHalfInteger = false;

        public static readonly String GAMMA_PARAM = "Gamma_param";

        public RandomGammaValue()
        {
            basicRandom = new RandomBasicValue();
        }
        public override double nextValue()
        {
            if (isIntegerType)
                return nextInteger();
            if (isHalfInteger)
                return nextHalfInteger();
            //TODO: доделать
            return 0;
        }

        private double nextInteger()
        {
            double value = 0;
            for (int i = 0; i < param; i++)
                value += -Math.Log(basicRandom.nextValue());
            return value;
        }

        private double nextHalfInteger()
        {
            double value = normalRandom.nextValue();
            value = value * value / 2;
            return nextInteger() + value;
        }

        public override JObject store()
        {
            return base.store();
        }

        public override void restore(JObject state)
        {
            param = (double)state.GetValue(GAMMA_PARAM);
        }

        public override void init()
        {
            if (param % 1 < Utils.EPSILON)
                isIntegerType = true;
            else if (Math.Abs(param % 1 - 0.5) < Utils.EPSILON)
            {
                isHalfInteger = true;
                normalRandom = new RandomNormalValue();
            }
            else
            {
                //TODO: доделать
                throw new CreateModelException("not implemented");
            }
        }

        public override void validate()
        {
            throw new NotImplementedException();
        }
    }
}
