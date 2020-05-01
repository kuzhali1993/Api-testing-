using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.TestData_Repository
{
    class Urlpath :BaseClass 
    {
        #region Categories
        public static string GetCategories = GetConfigValue("BaseUrl")+ "/api/categories?projectDocumentVersionId=";
        #endregion
    }
}
