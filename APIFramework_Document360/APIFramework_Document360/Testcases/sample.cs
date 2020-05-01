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
    class sample : BaseClass
    {
        public string url;
        
        [SetUp]
        public void Initialize()
        {
            LoadVariable();
            url = Urlpath.GetCategories + EnvironmentVariable["ProjectversionId"];

        }


        [Test]
        public void GetCategories_WithValidData()
        {
            Headers.Add("Authorization", EnvironmentVariable["AuthorizationToken"]);
            Headers.Add("ProjectId", EnvironmentVariable["ProjectId"]);
            var response = WebClient.GetResponse(url,Headers);
            //Response code
           Snippets.AssertResponseCode(response, 200);
            //Snippets.AssertResponseCode(response, 401);
            // Console.WriteLine((int)response.StatusCode);
            Snippets.CheckJsonValue(response, "categories[0].title", "new category gtrytr yt");
        }
    }
}
