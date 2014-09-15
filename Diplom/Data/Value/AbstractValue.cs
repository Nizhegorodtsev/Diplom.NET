using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Diplom.Data.Value
{
    /// <summary>
    /// Абстракция случайной величины, распределенной по некоторому закону
    /// </summary>
    abstract class AbstractValue : AbstractStorable
    {
        /// <summary>
        /// Получить значение случайной величины
        /// </summary>
        /// <returns>следующее значение</returns>
        public abstract double nextValue();
    }
}
