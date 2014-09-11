using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data.Business
{
    abstract class AbstractModel : AbstractStorable
    {
        protected bool run = false;

        protected int countOfModelCicle = 0;

        protected int currentModelCicle = 0;

        protected double currentTime = 0;

        protected String modelName = "No name";

        public static readonly String COUNT_OF_MODEL_CICLE = "Count_of_model_cicle";

        public static String MODEL_NAME = "Model_name";

        /// <summary>
        /// Запуск процесса моделирования
        /// </summary>
        public void startRun()
        {
            run = true;
            while (run)
                doStep();
            finish();
        }

        /// <summary>
        /// Остановка процесса моделирования
        /// </summary>
        public void stopRun()
        {
            run = false;
        }

        /// <summary>
        /// Итерация работы модели
        /// </summary>
        public abstract void doStep();

        /// <summary>
        /// Окончание процесса моделирования
        /// </summary>
        public abstract void finish();

        public override JObject store()
        {
            JObject state = base.store();
            state.Add(MODEL_NAME, modelName);
            return state;
        }

        public override void restore(JObject state)
        {
            modelName = (String)state.GetValue(MODEL_NAME);
        }
    }
}
