using NUnit.Framework;
using System;
using APIFramework_Document360.TestData_Repository;
using APIFramework_Document360.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APIFramework_Document360.Testcases
{
    [TestFixture,Order(4)]
    class sample : BaseClass
    {
        public sample()
        {
            Excel.OpenExcelSheet("Categories");
        }
       

         public string url = "https://api.document360.info//api/settings/add-project";
        public string geturl,payload;
            
        [SetUp]
        public void Initialize()
        {
            
            geturl = Urlpath.GetCategories + GetEnvironmentVariable("ProjectversionId");
            
        }

        [Test,Order(2)]
        public void GetCategories_WithValidData()
        {
            Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            var response = WebClient.GetResponse(geturl,Headers);
            
            //Response code
           Snippets.AssertResponseCode(response, 200);
            
         //   Snippets.AssertJsonValue(response, "categories[0].title",Condition.Equal,"new category gtrytr yt");
            Snippets.CheckBodyContains(response, "kuzhali");

            
        }


        [TestCase("properName")]
        [TestCase("WithspecChar&^%")]
        [TestCase("With-Hypen")]
        [TestCase("123456")]
        [TestCase("With space")]
        [Test,Order(1)]
        public void Addproject(string name)
        {
            Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            payload = Excel.GetRequestBoby("Addproject");
            var body = Snippets.SetJsonvalue(payload,"pname", name);
            var response = WebClient.PostRequest(url, Headers, body);
            Snippets.AssertResponseCode(response, 200);
        }

        [TearDown]
        public void close()
        {
           

        }
    }
}
