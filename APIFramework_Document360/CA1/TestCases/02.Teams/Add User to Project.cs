using APIFramework_Document360;
using APIFramework_Document360.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1.TestCases._02.Teams
{
    [TestFixture,Order(5)]
    [Category("Teams")]
    public class Add_User_to_Project:BaseClass
    {
        public string url, payload, schema;

        [OneTimeSetUp]
        public void setup()
        {
            var apidata = TestDataAccess.GetApiData("Teams", "Get all users");
            url = GetEnvironmentVariable("BaseUrl") + apidata.path;
            schema = apidata.schema;
            payload = apidata.RequestBoby;
        }
        [Test,Property("test scenarios","test TS in log")]
        public void Verify_AddUser_ResponseCode()
        {
           payload = TestDataAccess.GetrequestBody("Teams", "Verify user can add new user");
            var response = WebClient.PostRequest(url, Headers, payload);
            Snippets.AssertResponseCode(response, 200);
        }

    }
}
