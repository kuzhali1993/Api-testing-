using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.TestData_Repository
{
    class Urlpath : BaseClass
    {
        #region Categories
        public static string GetCategories = GetEnvironmentVariable("BaseUrl") + "/api/categories?projectDocumentVersionId=";
        public static string AddCategory = GetEnvironmentVariable("BaseUrl") + "/api/categories";
        public static string DeleteCategory = GetEnvironmentVariable("BaseUrl") + "/api/categories/delete?categoryName=";
        #endregion

        #region Articles
        public static string GetArticleById = GetEnvironmentVariable("BaseUrl") + "/api/articles/";
        #endregion

        #region Project version
        public static string GetAllVersion = GetEnvironmentVariable("Baseurl") + "/api/Settings/get-project-documentversion/"+ GetEnvironmentVariable("ProjectId");
        public static string Addprojectversion = GetEnvironmentVariable("Baseurl") + "/api/Settings/add-project-documentversion";
        #endregion
    }


}

