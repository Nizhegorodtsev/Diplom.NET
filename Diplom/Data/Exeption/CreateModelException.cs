using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Diplom.Data.Exeption
{
    /// <summary>
    /// Ошибка при создании модели
    /// </summary>
    class CreateModelException : Exception
    {
        public CreateModelException() : base() { }

        public CreateModelException(string message)
            : base(message) { }
    }
}
