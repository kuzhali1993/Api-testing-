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
    [TestFixture,Order(4)]
    [Category("Teams")]
    public class Get_All_Users : BaseClass
    {
        //variable declaration

        public string url, payload, schema;
        
        public Get_All_Users()
        {
           
        }

        [OneTimeSetUp]
        public void setup()
        {
            var apidata = TestDataAccess.GetApiData("Teams", "Get all users");
            url = GetEnvironmentVariable("BaseUrl") + apidata.path;
            schema = apidata.schema;
            payload = apidata.RequestBoby;
            
        }

        [Test]
        public void Veriy_ResponseCode()
        {
            var response = WebClient.GetResponse(url, Headers);
            Snippets.AssertResponseCode(response, 200);
        }

        [Test]
        public void Verify_ResponseSchema()
        {
            var response = WebClient.GetResponse(url, Headers);
            Snippets.ValidateJsonschema(response, schema);
        }

        [Test]
        public void Verify_ResponseSuccessData()
        {
            var response = WebClient.GetResponse(url, Headers);
            Snippets.AssertJsonValue(response, "success",Condition.Equal, "true");
        }
    }
}
