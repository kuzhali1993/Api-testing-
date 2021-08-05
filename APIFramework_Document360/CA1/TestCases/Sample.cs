using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIFramework_Document360;
using APIFramework_Document360.Utilities;
using APIFramework_Document360.ExcelData;
using NUnit.Framework;

namespace CA1.TestCases
{

    class Sample:BaseClass
    {
        [Test]
        public void samplet()
        {
            Console.WriteLine("kuzhali");
            Console.WriteLine(GetEnvironmentVariable("name"));
        }
        //public static string test;
        string url = "https://google.com";
        [Test]
        public void demo()
        {
            string str = GetEnvironmentVariable("name");
            Assert.AreEqual(str, "kuzhali123");
            var response = WebClient.GetResponse(url,Headers);
            
            
        }

        [Test]
        public void dataset()
        {
            
            String st = "Create category for proper data";
            //foreach(var i in data)
            //{
            //    Console.WriteLine(i.TestScenarios);
            //}
            
           var t= ExcelDataset.Category.Where(m => m.TestScenarios.Equals(st)).FirstOrDefault();
            if (t == null)
            {
                Assert.Fail("No data ");
            }
            Console.WriteLine(t.ReponseBody);
            //Console.WriteLine(t.RequestBoby);

        }
        [Test]
        public void t()
        {
            var t=ExcelDataset.Category.Where(m => m.TestScenarios.Equals("Create Sub category")).FirstOrDefault();
            
            Console.WriteLine("------------------");
            Console.WriteLine(t.ReponseBody);
        }
        
    }
}
