using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data
{
    /// <summary>
    /// Хранит информацию о некотором классе
    /// </summary>
    class ClassInfo
    {
        /// <summary>
        /// Имя класса
        /// </summary>
        private String name;

        /// <summary>
        /// Тип. Что-то в роде Class<?> в Java. Хранит кучу информации о классе. Помогает получить объект класса
        /// </summary>
        private Type type;

        /// <summary>
        /// Тип базового класса
        /// </summary>
        private Type baseClassType;

        /// <summary>
        /// Имя класса, которое будет использоваться при отображении
        /// </summary>
        private String displayName;

        /// <summary>
        /// Описание класса. Будет использоваться при отображении
        /// </summary>
        private String description;

        public String getName()
        {
            return name;
        }

        public void setName(String className)
        {
            this.name = className;
        }

        public Type getBaseClassType()
        {
            return baseClassType;
        }

        public void setBaseClassType(Type classType)
        {
            this.baseClassType = classType;
        }

        public Type getType()
        {
            return type;
        }

        public void setType(Type classType)
        {
            this.type = classType;
        }

        public String getDescription()
        {
            return description;
        }

        public void setDescription(String classDescription)
        {
            this.description = classDescription;
        }
    }
}
