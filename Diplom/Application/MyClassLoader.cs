using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

namespace Diplom.Data
{
    class MyClassLoader
    {
        private static Dictionary<String, ClassInfo> classMap = new Dictionary<String, ClassInfo>();

        // private static List<ClassInfo> classInfo = new List<ClassInfo>();

        public static void loadChildClasses(Type baseType)
        {
            IEnumerable<Type> list = Assembly.GetAssembly(baseType).GetTypes().Where(type => type.IsSubclassOf(baseType));
            foreach (Type type in list)
            {
                ClassInfo info = new ClassInfo();
                info.setType(type);
                info.setBaseClassType(baseType);
                info.setName(type.Name);
                classMap.Add(type.Name, info);
                Console.WriteLine(type);
            }
        }

        public static Type getTypeByName(String name)
        {
            ClassInfo info;
            classMap.TryGetValue(name, out info);
            if (info == null)
                return null;
            return info.getType();
        }

        public static List<ClassInfo> getClassInfoByBaseType(Type baseType)
        {
            List<ClassInfo> classList = new List<ClassInfo>();
            foreach (KeyValuePair<String, ClassInfo> entry in classMap)
            {
                ClassInfo info = entry.Value;
                if (info.getBaseClassType().Equals(baseType))
                    classList.Add(info);
            }
            return classList;
        }


    }
}
