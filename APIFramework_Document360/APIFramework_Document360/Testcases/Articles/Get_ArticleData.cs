using APIFramework_Document360.TestData_Repository;
using APIFramework_Document360.Utilities;
using NUnit.Framework;

namespace APIFramework_Document360.Testcases
{
    [TestFixture, Order(5)]


    class Get_ArticleData : BaseClass
    {
        public string url = Urlpath.GetArticleById + GetEnvironmentVariable("ArticleId");

        [Test, Order(1)]
        public void GetArticle_withProperdata()
        {
            Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            var response = WebClient.GetResponse(url, Headers);
            Snippets.AssertResponseCode(response, 200);
        }
    }
}
