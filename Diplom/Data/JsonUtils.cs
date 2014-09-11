using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data
{
    class JsonUtils
    {
        /// <summary>
        /// Создать список объектов из json массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static List<AbstractStorable> restoreArray(JArray array)
        {
            List<AbstractStorable> list = new List<AbstractStorable>();
            foreach (JObject obj in array.Children<JObject>())
            {
                list.Add(AbstractStorable.newInstance(obj));
            }
            return list;
        }

        /// <summary>
        /// Сохранить список объектов в виде json массива
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static JArray storeArray(List<AbstractStorable> list)
        {
            JArray array = new JArray();
            foreach (AbstractStorable obj in list)
                array.Add(obj.store());
            return array;
        }
    }
}
