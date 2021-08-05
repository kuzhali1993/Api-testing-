using NUnit.Framework;
using APIFramework_Document360.TestData_Repository;
using APIFramework_Document360.Utilities;
using System;

namespace APIFramework_Document360.Testcases
{
    [TestFixture,Order(1)]
    class GetAllVersions : BaseClass
    {
        public GetAllVersions()
        {
           
        }

        //Variable declaration
        public string url = Urlpath.GetAllVersion;
       [Test]
       public void sample()
        {
            Console.WriteLine(url);
        }

        //Testcases
        [Test]
        public void VerifywithoutHeaders()
        {
            
            var response = WebClient.PostRequest(url);
            Snippets.AssertResponseCode(response, 401);
        }
        [Test]
        public void VerifywithoutAuthToken()
        {
            LocalHeader.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            var response = WebClient.GetResponse(url, LocalHeader);
            Snippets.AssertResponseCode(response, 401);
        }
        [Test]
        public void VerifywithoutProjectId()
        {
            LocalHeader.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            var response = WebClient.GetResponse(url, LocalHeader);
            Snippets.AssertResponseCode(response, 401);
        }

        [Test]
        [Category("smokecase")]
        public void VerifywithProperHeader()
        {
            var response = WebClient.GetResponse(url, Headers);
            Snippets.AssertResponseCode(response, 200);
        }

        
    }
}
