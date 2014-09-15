using Diplom.Data.Business;
using Diplom.Data.Process;
using Diplom.Data.Value;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Data
{
    class AppData
    {
        private static AppData instance;

        public AppData()
        {
            MyClassLoader.loadChildClasses(typeof(AbstractModel));
            MyClassLoader.loadChildClasses(typeof(AbstractProcess));
            MyClassLoader.loadChildClasses(typeof(AbstractValue));
            MyClassLoader.loadChildClasses(typeof(AbstractBusinessProcess));
        }

        public void startmodel(String filePath)
        {
            String file = null;

            try
            {
                file = System.IO.File.ReadAllText(filePath);
                AbstractModel model = (AbstractModel)AbstractStorable.newInstance(JObject.Parse(file));
                model.startRun();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static AppData getInstance()
        {
            if (instance == null)
            {
                if (instance == null)
                {
                    instance = new AppData();
                }
            }
            return instance;
        }
    }
}
