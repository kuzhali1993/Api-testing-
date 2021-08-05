using APIFramework_Document360;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIFramework_Document360;
using APIFramework_Document360.ExcelData;
using APIFramework_Document360.Utilities;

namespace CA1
{
    [SetUpFixture]
    class OneTimeConfig
    {
        [OneTimeSetUp]
        public void onetimeconfig()
        {
            Console.WriteLine("onetime setup in CA1");
            BaseClass.Headers.Add("api_token", BaseClass.GetEnvironmentVariable("token"));
            //Baseclass.Headers.Add("api_token", GetEnvironmentVariable("token"));
            //ExcelActions.dynamicSheets();
            //ExcelDataset.ProjectVersion = ExcelActions.readExcel("ProjectVersions");
            TestDataAccess.ReadsheetData();
        }

        [OneTimeTearDown]
        public void lastfun()
        {
            Console.WriteLine("onetime teardown in cA1");
        }

    }
}
