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
    
    [TestFixture]
    [Order(1)]
    class Categories : BaseClass
    {
        public Categories()
        {
            Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            Excel.OpenExcelSheet("Categories");
        }


        // Variable Initialzation 
        private string url;
        private string payload;
      
        [OneTimeSetUp]
        public void prefetch()
        {
            Console.WriteLine("onetimesetup method");
        }

        

        [Test, Order(1)]
        [Category("Create_category")]

        public void CreateCategory_ForvalidData()
        {
            url = Urlpath.AddCategory;
            payload = Excel.GetRequestBoby("Create category for proper data");
           
            
             var response = WebClient.PostRequest(url,Headers, payload);
            
            //Response code check
            Assert.Multiple(() =>
            {
                Snippets.CheckResponseHeader(response, "Content-Type");
                Snippets.AssertResponseCode(response, 200);
                //Check Json value
                Snippets.AssertJsonValue(response, "category.categoryTitle", Condition.Equal, GetEnvironmentVariable("CategoryName"));
                Snippets.SetJsonpathvalueAsVariable(response, "category.categoryId", "CategoryId");
            });


        }

        [Category("Create_category")]
        public void CreateCategory_ForInvalidData()
        {
            
            Headers.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            Headers.Add("ProjectId", GetEnvironmentVariable("ProjectId"));
            payload = Excel.GetRequestBoby("Creatyhrttegory for proper data");

            var response = WebClient.PostRequest(url, Headers, payload);
            //Response code check
            Assert.Multiple(() =>
            {
                Snippets.AssertResponseCode(response, 401);
                //Check Json value
                Snippets.AssertJsonValue(response, "categories[0].title", Condition.Equal, "new category gtrytr yt");
            });
        }

        [Test,Order(2)]
        [Category("Delete category")]
        public void DeleteCategory()
        {
            url = Urlpath.DeleteCategory+GetEnvironmentVariable("CategoryName");
            payload = Excel.GetRequestBoby("Delete Category");
            Console.WriteLine(payload);
            var response = WebClient.PostRequest(url, Headers, payload);
            Snippets.AssertResponseCode(response, 200);
        }
        
    }
}
