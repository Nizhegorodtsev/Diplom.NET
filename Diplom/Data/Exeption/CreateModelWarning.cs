using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Exeption
{
    /// <summary>
    /// Предупреждение о том, что работа модели может быть некорректна
    /// </summary>
    class CreateModelWarning : Exception
    {
        public CreateModelWarning() : base() { }

        public CreateModelWarning(string message)
            : base(message) { }
    }
}
