using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ZhongTool.Common
{
    public static class JsonHelper
    {
        public static void SerializeObject(object obj,string jsonFilePath)
        {
            string str = JsonConvert.SerializeObject(obj);
            if (!File.Exists(jsonFilePath))
            {
                using (FileStream fs = File.Create(jsonFilePath))
                {              
                }
            }
            File.WriteAllText(jsonFilePath,str,Encoding.UTF8);
        }

        public static T DeserializeObject<T>(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                return default(T);
            }

            string str = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}