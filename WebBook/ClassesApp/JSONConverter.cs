using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace WebBook.ClassesApp
{
    public class JSONConverter
    {
        public static string ConvertJSTo<T>(T clas) 
        {
            string strok = JsonSerializer.Serialize(clas);
            return strok;
        }
        public static T ConvertJSOut<T>(string str)
        {
            T class1 = JsonSerializer.Deserialize<T>(str);
            return class1;
        }

        public void JsonGet() 
        {
            
        }

        public void JsonSet()
        {

        }
    }
}
