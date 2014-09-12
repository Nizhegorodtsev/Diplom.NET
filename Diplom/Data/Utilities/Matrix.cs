using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Utilities
{
    class Matrix : AbstractStorable
    {
        double[][] matrix;

        public double get(int i, int j)
        {
            return matrix[i][j];
        }

        public override void init()
        {
        }

        public override void restore(JObject state)
        {
            //  base.restore(state);
        }

        public override JObject store()
        {
            return base.store();
        }

        public override void validate()
        {
        }



    }
}
