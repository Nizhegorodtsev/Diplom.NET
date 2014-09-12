using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Diplom.Data.Random
{
    class RandomGammaValue:AbstractRandomValue
    {
        private double param1;

        public double param2;

        public RandomGammaValue()
        {
        }
        public override double nextValue()
        {
            throw new NotImplementedException();
        }

        public override JObject store()
        {
            return base.store();
        }

        public override void restore(JObject state)
        {
            throw new NotImplementedException();
        }

        public override void init()
        {
            throw new NotImplementedException();
        }

        public override void validate()
        {
            throw new NotImplementedException();
        }
    }
}
