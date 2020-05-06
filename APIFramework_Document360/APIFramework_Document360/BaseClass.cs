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

namespace APIFramework_Document360
{
    public class BaseClass
    {
      //  public Dictionary<string, dynamic> EnvironmentVariable = new Dictionary<string, dynamic>();
        public Dictionary<string, string> Headers = new Dictionary<string, string>();

        //Condition enum for Jsonvalidation
        public enum Condition
        {
            Equal,
            NotEqual,
            Contains
        }

        public ExtentReports Report;
        public string TestCategory;

       [OneTimeSetUp]
       public void BeforeClass()
        {
           
            Report = Reporter.getInstance();
            

            Reporter.CreateLogforApi(this.GetType().Name);
        }
        
        [SetUp]
        public void BeforeEachTest()
        {
            
            Reporter.CreateLogforTest();
        }
        [TearDown]
        public void AfterEachTest()
        {
            Headers.Clear();
            Reporter.LogResult();
        }

        [OneTimeTearDown]
        public void cleanall()
        {
            Reporter.Flushreport();
        }
     
        //public void LoadVariable()
        //{
        //    var appsettings = ConfigurationManager.GetSection("appSettings") as NameValueCollection;


        //    if (appsettings.Count == 0)
        //    {
        //        Console.WriteLine("No Environment defined in given config file");
        //        EnvironmentVariable = null;
        //    }
        //    else
        //    {

        //        foreach (var key in appsettings.AllKeys)
        //        {
        //            EnvironmentVariable.Add(key, appsettings[key]);
        //        }
        //    }

        //}

        public static string GetEnvironmentVariable(string key)
        {
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

        public static string GetFolderPath(string fname)
        {
            
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpath = path.Substring(0, path.LastIndexOf("bin")) + fname;
            string localpath = new Uri(finalpath).LocalPath;
            return localpath;
        }
    }
}
