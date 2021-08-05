using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using APIFramework_Document360.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AventStack.ExtentReports;
using System.IO;
using APIFramework_Document360.ExcelData;

namespace APIFramework_Document360
{
    public class BaseClass
    {

      // public  Dictionary<string, dynamic> EnvironmentVariable = new Dictionary<string, dynamic>();
        public static Dictionary<string, string> Headers = new Dictionary<string, string>();
        public static Dictionary<string, string> LocalHeader = new Dictionary<string, string>();
        private static Dictionary<string, dynamic> EnvironmentVariable;
        public static Dictionary<string, dynamic> variables;

        //Condition enum for Jsonvalidation
        public enum Condition
        {
            Equal,
            NotEqual,
            Contains
        }
        // Excel.Category = UserData.readExcel();
        

        public ExtentReports Report;
        public string TestCategory;
        

       [OneTimeSetUp]
       public void BeforeClass()
        {
            Console.WriteLine("Onetimesetup");
            Report = Reporter.getInstance();
            Reporter.CreateLogforApi(this.GetType().Name);
             //Excel.GetExcelInstance();
            //Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            //Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
           //ExcelDataset.Category = ExcelActions.readExcel("Categories");
            
        }

       // public abstract string Opensheet(string sheetname);
        
        [SetUp]
        public void BeforeEachTest()
        {
            Console.WriteLine("Before each test");
            Reporter.CreateLogforTest();
        }
        [TearDown]
        public void AfterEachTest()
        {
            Console.WriteLine("after test");
            if((LocalHeader.Count)>0)
            
            LocalHeader.Clear();
            Reporter.LogResult();
        }

        [OneTimeTearDown]
        public void Afterclass()

        {
            Console.WriteLine("one time teardown");
            //if ((Headers.Count) > 0)
            //    Headers.Clear();
            Reporter.Flushreport();
           Excel.CloseExcel();
        }

        public static Dictionary<string, dynamic> GetcurrentVariables()
        {
            var appsettings = ConfigurationManager.GetSection("appSettings") as NameValueCollection;
            EnvironmentVariable = new Dictionary<string, dynamic>();
            //EnvironmentVariable = null;
            try
            {
                if (appsettings.Count == 0)
                {
                    Console.WriteLine("No Environment defined in given config file");
                    return null;
                }
                else
                {

                    foreach (var key in appsettings.AllKeys)
                    {
                        EnvironmentVariable.Add(key, appsettings[key]);
                    }
                }

                return EnvironmentVariable;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        public static string GetEnvironmentVariable(string key)
        {
            ConfigurationManager.RefreshSection("appSettings");
            try
            {
                string str_value = ConfigurationManager.AppSettings.Get(key);
                return str_value;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static void SetEnvironmentVariable(string key,string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
           if(config.AppSettings.Settings.AllKeys.Contains(key))
            {
                config.AppSettings.Settings[key].Value = value;
            }
           else
            {
                config.AppSettings.Settings.Add(key, value);
            }
            
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        public static string GetFolderPath(string fname)
        {
            
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpath = path.Substring(0, path.LastIndexOf("bin")) + fname+"\\";
            string localpath = new Uri(finalpath).LocalPath;
            return localpath;
        }

        public static string ReplaceEnvVariable(string json)
        {
            string key; 
        variables = GetcurrentVariables();
            foreach (var items in variables)
            {
                key = "{{" + items.Key + "}}";
                if (json.Contains(key))
                {

                    json = json.Replace(key, items.Value);
                }

            }
            variables = null;
            return json;
        }
    }
}
