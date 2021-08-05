using APIFramework_Document360;
using APIFramework_Document360.ExcelData;
using APIFramework_Document360.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1.TestCases._01.Project_Versions
{
    [TestFixture,Order(1)]
    [Category("Project Versions")]
    class Get_All_Versions:BaseClass
    {
        //variable declaration
        public string url,schema,payload;
        //string apidetail;
        //Constructor
       public Get_All_Versions()
        {
         // var t = ExcelDataset.Sheets["ProjectVersions"].Where(m => m.APIName.Contains("Get all versions")).FirstOrDefault();
            var apidata = TestDataAccess.GetApiData("ProjectVersions", "Get all versions");
            url = GetEnvironmentVariable("BaseUrl") + apidata.path;
            schema = apidata.schema;
            payload = apidata.RequestBoby;
            

            //Console.WriteLine(apidetail);
        }

        [Test]
        public void tt()
        {
            Console.WriteLine("get all version");
            Console.WriteLine(Headers.Count);
            //Console.WriteLine(ExcelDataset.ProjectVersion)
            Console.WriteLine(url);
            payload = TestDataAccess.GetrequestBody("ProjectVersions", "Verify for proper data");
            var response = WebClient.GetResponse(url,Headers);
            Snippets.AssertResponseCode(response, 200);

        }
    }
}
