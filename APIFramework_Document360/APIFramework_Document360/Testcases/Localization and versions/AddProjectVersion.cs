using NUnit.Framework;
using APIFramework_Document360.TestData_Repository;
using APIFramework_Document360.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.Testcases
{
    [TestFixture,Order(2)]
    [Category("AddVersions")]
    class AddProjectVersion : BaseClass
    {
        public AddProjectVersion()
        {
            Excel.OpenExcelSheet("Projectversion");
        }

        //Variable declaration
        public static string url = Urlpath.Addprojectversion;
        public static string getversionurl = Urlpath.GetAllVersion;
        public static string payload, schema;
           

        // Testcases
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
            payload = Excel.GetRequestBoby("Verify create fresh version");
          
            var response = WebClient.PostRequest(url,LocalHeader,payload);
            Snippets.AssertResponseCode(response, 401);
        }
        [Test]
        public void VerifywithoutProjectId()
        {
            LocalHeader.Add("Authorization", GetEnvironmentVariable("AuthorizationToken"));
            payload = Excel.GetRequestBoby("Verify create fresh version");
            var response = WebClient.PostRequest(url, LocalHeader, payload);
            Snippets.AssertResponseCode(response, 401);
        }

        [Test]
        [Category("smokecase")]
        public void VerifywithProperHeader()
        {
            payload = Excel.GetRequestBoby("Verify create fresh version");
            var response = WebClient.PostRequest(url, Headers, payload);
            Assert.Multiple(() =>
            {
                Snippets.AssertResponseCode(response, 200);
                Snippets.AssertJsonValue(response, "success", Condition.Equal, "true");
               // Snippets.SetJsonpathvalueAsVariable(response, "projectDocumentVersionId", "ProjectversionId");
                // get the response
            }
            );
            
        }

        [Test]
        [Category("smokecase")]
        public void VerifyAddVersion_Fresh()
        {
            payload = Excel.GetRequestBoby("Verify create fresh version");
            schema = Excel.GetRequestBoby("schema");
            
            var response = WebClient.PostRequest(url, Headers, payload);
            Assert.Multiple(() =>
            {
                Snippets.AssertResponseCode(response, 200);
                Snippets.AssertJsonValue(response, "success", Condition.Equal, "True");
                Snippets.SetJsonpathvalueAsVariable(response, "projectDocumentVersionId", "ProjectversionId");
                // get the response
                
                var getresponse = WebClient.GetResponse(getversionurl, Headers);
                //Console.WriteLine(Snippets.GetElementvalue(getresponse, "projectDocumentVersions[2].id"));
                for(int i=0;i< Snippets.GetElementCount(getresponse, "projectDocumentVersions");i++)
                {
                    if (Snippets.GetElementvalue(getresponse, "projectDocumentVersions[" + i + "].id") == GetEnvironmentVariable("ProjectversionId"))
                        Snippets.AssertJsonValue(getresponse, "projectDocumentVersions[" + i + "].createdAt", Condition.NotEqual, "null");

                }
                Snippets.ValidateJsonschema(getresponse, schema);

                
            }
            );

        }

        //inner class
        [TestFixture]
        [Category("AddVersions")]
        public class AddProjectVersion_Fresh: BaseClass
        {
            //AddProjectVersion obj = new AddProjectVersion();
           [Test]
           public void samplee()
            {
                var response = WebClient.GetResponse(getversionurl, Headers);
                Snippets.AssertResponseCode(response, 200);
            }


        }

    }

    
}
