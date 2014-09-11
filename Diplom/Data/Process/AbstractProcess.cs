using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Process
{
    /// <summary>
    /// Абстракция математического процесса, который генерирует время наступления событий
    /// </summary>
    abstract class AbstractProcess : AbstractStorable
    {
        /// <summary>
        /// Генерация времени, через которое наступит следующее событие
        /// </summary>
        /// <returns></returns>
        abstract public double nextValue();
    }
}
