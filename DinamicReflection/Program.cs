using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DinamicReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFile(@"\\dc\Студенты\ПКО\SEB-171.2\C#\Exception\GeneratorName.dll");
            Type[] types = assembly.GetTypes();
            #region
            foreach (Type item in types)
            {
                Console.WriteLine("-> {0} ({1})", item.Name, item.IsClass);
                foreach (MethodInfo methods in item.GetMethods())
                {
                    Console.WriteLine(" -> {0} ({1})", methods.Name, methods.ReturnType);
                    foreach (ParameterInfo param1 in methods.GetParameters())
                    {
                        Console.WriteLine("   -> {0} ({1})", param1.Name, param1.ParameterType.BaseType);
                    }
                }
            }
            #endregion
            Type tGenerator = types.FirstOrDefault(f => f.IsClass && f.Name == "Generator");
            //exsempliar class
            Object metGenerator = Activator.CreateInstance(tGenerator);
            //method Generator
            MethodInfo GenerateDefault = metGenerator.GetType().GetMethod("GenerateDefault");
            //parametry methoda
            ParameterInfo piGender = GenerateDefault.GetParameters()[0];
            
            object gender = null;

            if (piGender.ParameterType.BaseType == typeof(Enum))
            {
                gender = Enum.ToObject(piGender.ParameterType,0);
                //spisok enumov
                FieldInfo[] fiGender = piGender.ParameterType.GetFields(BindingFlags.Public | BindingFlags.Static);
                foreach (var item in fiGender)
                {
                    Console.WriteLine(item.Name);
                }
            }
           

            object[] param = new object[] { };
            var result = GenerateDefault.Invoke(tGenerator, param);

        }
    }
}
