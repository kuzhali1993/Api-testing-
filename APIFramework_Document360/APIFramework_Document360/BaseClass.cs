using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360
{
    public class BaseClass
    {
        public Dictionary<string, dynamic> EnvironmentVariable = new Dictionary<string, dynamic>();
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public void LoadVariable()
        {
            var appsettings = ConfigurationManager.GetSection("appSettings") as NameValueCollection;


            if (appsettings.Count == 0)
            {
                Console.WriteLine("No Environment defined in given config file");
                EnvironmentVariable = null;
            }
            else
            {

                foreach (var key in appsettings.AllKeys)
                {
                    EnvironmentVariable.Add(key, appsettings[key]);
                }
            }

        }

        public static string GetConfigValue(string key)
        {
            string str_value = ConfigurationManager.AppSettings.Get(key);
            return str_value;
        }
    }
}
