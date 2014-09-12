using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Diplom.Data.Utilities
{
    /// <summary>
    /// Разная функциональность для удобного парсинга и сохранения Json
    /// </summary>
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

        /// <summary>
        /// Восстановить вектор
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double[] restoreVector(JArray array)
        {
            double[] vector = new double[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                vector[i] = array[i].Value<Double>();
            }
            return vector;
        }


        /// <summary>
        /// Сохранить вектор
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static JArray storeVector(double[] vector)
        {
            JArray array = new JArray();
            for (int i = 0; i < vector.Length; i++)
                array.Add(vector[i]);
            return array;
        }

        /// <summary>
        /// Восстановить матрицу
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double[][] restoreMatrix(JArray array)
        {
            double[][] matrix = new double[array.Count][];
            int i = 0;
            foreach (JArray obj in array.Children<JArray>())
            {
                matrix[i] = parseVector(obj);
                i++;
            }
            return matrix;
        }

        /// <summary>
        /// Сохранить матрицу
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static JArray storeMatrix(double[][] matrix)
        {
            JArray array = new JArray();
            for (int i = 0; i < matrix.Length; i++)
                array.Add(storeVector(matrix[i]));
            return array;
        }
    }
}
