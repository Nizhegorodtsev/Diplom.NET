using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Diplom.Data.Exeption;

namespace Diplom.Data
{
    /// <summary>
    /// Асбтрактная сущность, состояние которой можно сохранить в JSON объект или восстановить из JSON объекта
    /// </summary>
    abstract class AbstractStorable
    {
        /// <summary>
        /// Этому ключу соответствует название класса объекта
        /// </summary>
        public static readonly String NAME = "Name";

        /// <summary>
        /// Этому ключу соответствует путь к директории, в которой хранится класс объекта
        /// </summary>
        public static readonly String TYPE = "Type";

        /// <summary>
        ///Восстановление состояния объекта, которое было сохранено в JSON. Этот метод предполагает исключительно инициализацию полей,
        ///информация которых была сохранена в JSON. Все дальнейшие мероприятия по инициализации объекта должны быть реализованы в
        /// методе init()
        /// </summary>  сохраренное состояние
        /// <param name="state"></param>
        public abstract void restore(JObject state);

        /// <summary>      
        /// Создание JSON объекта, в котором фиксируется состояние сохроняемой сущности
        /// </summary>
        /// <returns></returns>
        public virtual JObject store()
        {
            JObject state = new JObject();
            state.Add(NAME, getClassName());
            return state;
        }

        /// <summary>
        ///  Создание объекта из сохраненного ранее состояния в JSON объекте. Сначала создается экземпляр класса, затем	 
        ///  восстанавливается его состояние методом restore(). Этот метод также за восстановление всех простых полей и сложных объектов, 
        ///  которые принадлежат текущему объекту. Восстановление объекта можно представить в виде дерева. Далее проходит валидация созданного 
        ///  объекта и вызввается метод init(), который подготовит объекты к работе. Метод init() вызывается ПОСЛЕ того, как все объекты будут 
        ///  проинициализированны из-за специфики метода restore()
        /// </summary>
        /// <param name="state"> сохраненное состояние объекта </param>
        /// <returns></returns>
        public static AbstractStorable newInstance(JObject state)
        {
            Type type = MyClassLoader.getTypeByName((String)state.GetValue(NAME));
            if (type == null)
                throw new CreateModelException((String)state.GetValue(NAME) + " create instance exeption");
            System.Reflection.ConstructorInfo ci = type.GetConstructor(new Type[] { });
            AbstractStorable instance = (AbstractStorable)ci.Invoke(new object[] { });
            instance.restore(state);
            instance.validate();
            instance.init();
            return instance;
        }

        /// <summary>
        ///  Инициализация компонент класса, необходимых для работы, выполнение всех мероприятий, которые надо провести после того, как
        ///  объект был востановлен из JSON
        /// </summary>
        public abstract void init();


        /// <summary>
        /// 
        /// </summary>
        public abstract void validate();

        /// <summary>
        /// Получить название класса объекта
        /// </summary>
        /// <returns></returns>
        protected String getClassName()
        {
            String className = GetType().Name;
            //  className = className.substring(className.lastIndexOf("."));
            return GetType().Name;
        }

        /// <summary>
        /// Получить директорию, в которой расположен класс объекта
        /// </summary>
        /// <returns></returns>
        protected String getDirectory()
        {
            String directory = this.GetType().AssemblyQualifiedName;
            //   directory = directory.substring(0, directory.lastIndexOf("."));
            return directory;
        }
    }
}

