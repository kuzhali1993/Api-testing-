using APIFramework_Document360.TestData_Repository;
using APIFramework_Document360.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.Testcases.Category_Manager
{
    class CreateCategory :BaseClass
    {
        private string url = Urlpath.GetCategories + GetEnvironmentVariable("ProjectversionId");

        [Test, Order(1)]
        public void GetCategory_ForvalidData()
        {
            Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            var response = WebClient.GetResponse(url, Headers);
            //Response code check
            Snippets.AssertResponseCode(response, 200);
            //Check Json value
            Snippets.CheckJsonValue(response, "categories[0].title", Condition.Equal, "new category gtrytr yt");

        }
    }
}
