using NUnit.Framework;
using System;
using APIFramework_Document360.TestData_Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.Testcases
{
    class sample : BaseClass
    {
        [SetUp]
        public void Initialize()
        {
            LoadVariable();

        }
        

        [Test]
        public void sampletest()
        {
            String url = Urlpath.GetCategoryByID + EnvironmentVariable["BaseUrl"];
            Console.WriteLine(url);
        }
    }
}
