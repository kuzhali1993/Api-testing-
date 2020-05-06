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
            
            url = Urlpath.GetCategories + GetEnvironmentVariable("ProjectversionId");
           
        }


        [Test,Order(1)]
        public void GetCategories_WithValidData()
        {
            Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            var response = WebClient.GetResponse(url,Headers);
            
            //Response code
           Snippets.AssertResponseCode(response, 200);
            
         //   Snippets.CheckJsonValue(response, "categories[0].title",Condition.Equal,"new category gtrytr yt");
            Snippets.CheckBodyContains(response, "kuzhali");
        }

        [Test,Order(2)]
        public void GetCategories_Invaliddata()
        {
            //Headers.Add("Authorization", EnvironmentVariable["AuthorizationToken"]);
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            var response = WebClient.GetResponse(url, Headers);

            //Response code
            Snippets.AssertResponseCode(response, 200);

            //   Snippets.CheckJsonValue(response, "categories[0].title",Condition.Equal,"new category gtrytr yt");
            Snippets.CheckBodyContains(response, "kuzhali");
        }

        [TearDown]
        public void close()
        {
            // Reporter.LogResult();
            Headers.Clear();
            Console.WriteLine(Headers.Count);

        }
    }
}
