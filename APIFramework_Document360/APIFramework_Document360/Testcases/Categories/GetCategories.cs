using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIFramework_Document360.TestData_Repository;
using APIFramework_Document360.Utilities;
using NUnit.Framework;


namespace APIFramework_Document360.Testcases
{
    [TestFixture, Order(2)]
    class Get_Categories : BaseClass
    {
        private string url = Urlpath.GetCategories + GetEnvironmentVariable("ProjectversionId");
        public string catname = "category";
       
       

        [Test,Order(1)]
        public void GetCategory_ForvalidData()
        {
           
            Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            var response = WebClient.GetResponse(url, Headers);
            //Response code check
            Snippets.AssertResponseCode(response, 200);
            //Check Json value
            Snippets.AssertJsonValue(response, "categories[0].title", Condition.Equal, "new category gtrytr yt");

        }

        [Test, Order(2)]
        public void GetCategories_Invaliddata()
        {
            //Headers.Add("Authorization", EnvironmentVariable["AuthorizationToken"]);
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            var response = WebClient.GetResponse(url, Headers);

            //Response code
            Snippets.AssertResponseCode(response, 401);

           
            Snippets.CheckBodyContains(response, "kuzhali");
        }

        
    }
}
