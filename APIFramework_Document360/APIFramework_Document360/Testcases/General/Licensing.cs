using APIFramework_Document360.TestData_Repository;
using APIFramework_Document360.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APIFramework_Document360.Testcases
{
    
   //[TestFixture("65d45aa6-855c-4bcf-b8c5-5cc25e721134"),Order(3)]
    //[TestFixture("2795a339-0d99-4453-916e-c7c49635cf32")]
    //[TestFixtureSource()]
    class Licensing : BaseClass
    {
        public string url,payload;

        public string projid;
        public Licensing(string projectId)
        {
            Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            Headers.Add("ProjectId", projectId);
         this.projid = projectId;
            
        }
         
        [Test,Order(1)]
        public void GetCategory()
        {
            url = Urlpath.GetCategories + GetEnvironmentVariable("ProjectversionId");
            var response = WebClient.GetResponse(url, Headers);
            Console.WriteLine("project id -" + projid);
            Snippets.AssertResponseCode(response, 200);
        }

        [Test,Order(2)]
        public void CreateCategory()
        {
            url = Urlpath.AddCategory;
            
            payload = Excel.GetRequestBoby("Create category for proper data");
            var response = WebClient.PostRequest(url, Headers, payload);
            Console.WriteLine("project id -" + projid);
            Snippets.AssertResponseCode(response, 200);

        }
    }
}
