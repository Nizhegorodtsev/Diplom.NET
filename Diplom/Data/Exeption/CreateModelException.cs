using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Diplom.Data.Exeption
{
    /// <summary>
    /// Ошибка при восстановлении сущности из сохраненного JSON объекта
    /// </summary>
    class CreateModelException : Exception
    {
        public CreateModelException() : base() { }

        public CreateModelException(string message)
            : base(message) { }
    }
}
