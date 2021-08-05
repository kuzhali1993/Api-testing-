using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIFramework_Document360;

namespace CA1.TestData_Repro
{
    class UrlPath
    {
        #region Project Versions
        public static string Getallversion = BaseClass.GetEnvironmentVariable("Baseurl") + "/v1/ProjectVersions";
        #endregion

    }
}
