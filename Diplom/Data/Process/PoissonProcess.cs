using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diplom.Data.Random;
using Diplom.Data.Process;
using System.Threading.Tasks;
using Diplom.Data.Exeption;
using Diplom.Data.Utilities;

namespace Diplom.Data.Process
{
    /// <summary>
    /// Простейший Пуассоновский поток событий
    /// </summary>
    class PoissonProcess : AbstractProcess
    {
        private RandomExponentialValue randomValue;

        private double lambda;

        public static String LAMBDA = "Lambda";

        public PoissonProcess()
        {
            randomValue = new RandomExponentialValue();
        }

        public override double nextValue()
        {
            return randomValue.nextValue();
        }

        public override void restore(JObject obj)
        {
            lambda = (Double)obj.GetValue(LAMBDA);
        }

        public override JObject store()
        {
            JObject obj = base.store();
            obj.Add(LAMBDA, lambda);
            return obj;
        }

        public override void init()
        {
            randomValue.setLambda(lambda);
        }

        public override void validate()
        {
            if (lambda <= Utils.EPSILON)
                throw new CreateModelException("lambda is too small");
        }
    }
}
