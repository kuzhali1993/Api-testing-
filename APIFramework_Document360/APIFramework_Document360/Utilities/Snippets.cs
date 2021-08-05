using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.Utilities
{
    public class Snippets : BaseClass
    {
        private static JToken token;
        public static void AssertResponseCode(HttpResponseMessage response, int statuscode)
        {
            int codenum = (int)response.StatusCode;
            try
            {
                // Assert.AreEqual(statuscode, codenum, "Expected status code is not equal to actual code");
                if (codenum == statuscode)
                {
                    Reporter.testcaseLogger.Log(Status.Pass, " Status code check : Expected status code (" + statuscode + ")is equal to Actual code");
                }
                else
                {
                    Reporter.testcaseLogger.Log(Status.Fail, "Status code check : Expected status code (" + statuscode + ")is not equal to Actual code");
                    Assert.Fail("Status code validation failed\nExpected:" + statuscode + "\nActual:" + codenum);
                }
            }
            catch (Exception ex)
            {

                Reporter.testcaseLogger.Log(Status.Fail,ex.Message);
              //  Assert.Fail();
              //  Reporter.status = Reporter.TestcaseStatus.Fail;
            }
        }

        public static void AssertJsonValue(HttpResponseMessage response, string xpath, Condition cond, string value)
        {
            if (response.IsSuccessStatusCode)
            {
                token = Getxpath(response, xpath);
                if (token == null)
                {
                    Reporter.testcaseLogger.Log(Status.Fail, " Json value Check : Invalid Json path - " + xpath);
                   // Assert.Fail("Invalid Json path");
                }
                else
                {
                    switch (cond)
                    {
                        case Condition.Equal:
                            {
                                try
                                {
                                    //Assert.AreEqual(value, token.ToString());
                                    if (value.Equals(token.ToString()))
                                    {
                                        Reporter.testcaseLogger.Log(Status.Pass, "Given path [" + token.Path + "] has expected value" + token.ToString());
                                    }
                                    else
                                    {
                                        Reporter.testcaseLogger.Log(Status.Fail, "Expected value (" + value + ") is not equal to Actual value (" + token.ToString() + ") in path [" + token.Path + "]");
                                        Assert.Fail("Json value assertion Failed\nExpected:"+value+"\nActual:"+token.ToString());
                                    }
                                }
                                catch (Exception e)
                                {
                                    Reporter.testcaseLogger.Log(Status.Fail, "Assertion Exception: "+e.Message);
                                    Assert.Fail(e.Message);
                                }
                                break;
                            }
                        case Condition.NotEqual:
                            {
                                try
                                {
                                    Assert.AreNotEqual(value, token.ToString());
                                    Reporter.testcaseLogger.Log(Status.Pass, "Given path [" + token.Path + "] has value" + token.ToString());
                                }
                                catch (Exception e)
                                {
                                    Reporter.testcaseLogger.Log(Status.Fail, "Expected value (" + value + ") is equal to Actual value (" + token.ToString() + ") in path [" + token.Path + "]");
                                    // Assert.Fail("Actual values is equal to expected value but Actual condition : Not equal");
                                    Assert.Fail();
                                }
                                break;
                            }
                        default: break;

                    }
                }
            }

            else
            {
                Reporter.testcaseLogger.Log(Status.Fail, "Response content is invalid");
                Assert.Fail("Response json content is invalid");
            }
        }

        public static void CheckBodyContains(HttpResponseMessage response, string SearchKeyword)
        {
            try
            {
                string ResponseStr = (response.Content.ReadAsStringAsync().Result).ToString();

                Assert.IsTrue(ResponseStr.Contains(SearchKeyword), "Given search text is not present in response body");
                Reporter.testcaseLogger.Log(Status.Pass, "Response body contains the given text(" + SearchKeyword + ")");



            }
            catch (Exception ex)
            {
                Reporter.testcaseLogger.Log(Status.Fail, "Given text(" + SearchKeyword + ") is not present in Response body");

            }
        }

        public static void CheckReponseBodyEquals(HttpResponseMessage response, string responsebody)
        {
            try
            {
                string ResponseStr = (response.Content.ReadAsStringAsync().Result).ToString();

                Assert.IsTrue(ResponseStr.Equals(responsebody), "Response body is not equal to given value");
                Reporter.testcaseLogger.Log(Status.Pass, "Response body is Equal to given value");



            }
            catch (Exception ex)
            {
                Reporter.testcaseLogger.Log(Status.Fail, "Response body is not equal to given value");
                Assert.Fail();
            }
        }

        public static string SetJsonvalue(string payload, string oldvalue, string newValue)
        {
            oldvalue = "{{" + oldvalue + "}}";
            if (payload.Contains(oldvalue))
            {
                payload = payload.Replace(oldvalue, newValue);

            }
            return payload;
        }

        public static void CheckResponseHeader(HttpResponseMessage response, string headervalue)
        {
            try
            {
                var ResponseHeaders = response.Content.Headers;
                Console.WriteLine(ResponseHeaders);
            }
            catch (Exception e)
            {

            }
        }

        public static void SetJsonpathvalueAsVariable(HttpResponseMessage response, string xpath, string VariableName)
        {
            if (response.IsSuccessStatusCode)
            {
                token = Getxpath(response, xpath);
                if (token == null)
                {

                    Reporter.testcaseLogger.Log(Status.Info, " unable to set variable : Invalid Json path - " + xpath);
                    // Assert.Fail("Invalid Json path");
                    SetEnvironmentVariable(VariableName, null);
                }
                else
                {
                    SetEnvironmentVariable(VariableName, token.ToString());
                }
            }
            else
            {
                Reporter.testcaseLogger.Log(Status.Info, "Unable to set variable : Response content is invalid");
            }
        }

        public static int GetElementCount(HttpResponseMessage response, string xpath)
        {
            if (response.IsSuccessStatusCode)
            {
                token = Getxpath(response, xpath);
                if (token == null)
                    return 0;
                else
                return token.Count();
            }
            else
                return 0;
        }

        private static bool IsJsonSuccessTrue(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                token = Getxpath(response, "success");
                if (token == null)
                    return false;
                else if ((token.ToString()).Equals("false"))
                    return false;
                else

                    return true;
            }
            else
                return false;

        }

        private static JToken Getxpath(HttpResponseMessage response,string xpath)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var Jsoncontent = JObject.Parse(content);
            var token = Jsoncontent.SelectToken(xpath);
            return token;
        }

        public static dynamic GetElementvalue(HttpResponseMessage response, string xpath)
        {
            if (response.IsSuccessStatusCode)
            {
                token = Getxpath(response, xpath);
                if (token == null)
                {
                    Reporter.testcaseLogger.Log(Status.Info, "Invalid Json path -" +token.Path);
                    return null;
                }
                else
                    return token.ToString();
            }
            else
            {
                Reporter.testcaseLogger.Log(Status.Info, "Invalid Json path");
                return null;
            }
        }

        public static void ValidateJsonschema(HttpResponseMessage response,string schemastr)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var Jsoncontent = JObject.Parse(content);
                JSchema schema = JSchema.Parse(schemastr);
                IList<string> SchemaValidationErrors = new List<string>();
                try
                {
                    if(Jsoncontent.IsValid(schema,out SchemaValidationErrors))
                    {
                        Reporter.testcaseLogger.Log(Status.Pass, "Response schema is valid");
                       // Console.WriteLine("response schema is valid");
                    }
                    else
                    {
                        string error = "";
                        foreach(string str in SchemaValidationErrors)
                        {
                            error += (str+"\n");
                            
                            //Console.WriteLine("");
                        }
                        Reporter.testcaseLogger.Log(Status.Fail, "Json schema validation Failed - "+SchemaValidationErrors.Count+" errors\n"+error );
                        Assert.Fail("JsonSchema Validation Failed");
                    }
                }
                catch(Exception ex)
                {
                    Reporter.testcaseLogger.Log(Status.Fail, ex.Message);
                    Assert.Fail();
                }
                
            }
            else
            {
                Reporter.testcaseLogger.Log(Status.Info, "Unable to valid the JSonSchema : Response content is invalid");
            }
        }
    }
}
