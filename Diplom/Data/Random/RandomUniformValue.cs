using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Diplom.Data.Exeption;

namespace Diplom.Data.Random
{
    /// <summary>
    /// Равномерно распределенная случайная величина
    /// </summary>
    class RandomUniformValue : AbstractRandomValue
    {
        private RandomBasicValue brv;

        private double begin;

        private double end;

        public static String BEGIN = "Begin";

        public static String END = "End";

        public RandomUniformValue()
        {
            brv = new RandomBasicValue();
        }

        public override double nextValue()
        {
            return begin + (end - begin) * brv.nextValue();
        }

        public override void restore(JObject state)
        {
            begin = (Double)state.GetValue(BEGIN);
            end = (Double)state.GetValue(END);

        }

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(BEGIN, begin);
            state.Add(END, end);
            return state;
        }

        public override void init()
        {
        }

        public override void validate()
        {
            if (begin >= end)
                throw new CreateModelException("UniformRandomValue: begin >= end");
        }

    }
}
