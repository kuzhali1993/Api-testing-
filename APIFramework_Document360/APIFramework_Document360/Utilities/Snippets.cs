using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.Utilities
{
    class Snippets : BaseClass
    {
        public static void AssertResponseCode(HttpResponseMessage response,int statuscode)
        {
            int codenum = (int)response.StatusCode;
                Assert.AreEqual(statuscode,codenum,"Expected status code is not equal to actual code");
          
            
        }

        public static void CheckJsonValue(HttpResponseMessage response,string xpath,Condition cond,string value)
        {
            if(response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var Jsoncontent = JObject.Parse(content);
                var token =Jsoncontent.SelectToken(xpath);
                switch(cond)
                {
                    case Condition.Equal:
                        {
                            Assert.AreEqual(value, token.ToString(), "path value is not same");
                            break;
                        }
                    case Condition.NotEqual:
                        {
                            Assert.AreNotEqual(value, token.ToString(), "Excepted and actual values are equal");
                            break;
                        }
                    default: break;

                }
                
                //Console.WriteLine(token.ToString());
               
            }
            else
            {
                Assert.Fail("Response json content is invalid");
            }
        }

        public static void CheckBodyContains(HttpResponseMessage response,string SearchKeyword)
        {
            String ResponseStr = response.Content.ReadAsStringAsync().Result.ToString();
            if(ResponseStr.Contains(SearchKeyword))
            {
                Console.WriteLine("Textpresent");
            }
            else
            {
                Assert.Fail("Given Text is not present in Body");
            }
        }

        public static void CheckReponseBodyEquals(HttpResponseMessage response,string responsebody)
        {
            String ResponseStr = response.Content.ReadAsStringAsync().Result.ToString();
            Assert.IsTrue(ResponseStr.Equals(responsebody));
        }
       private static dynamic Getjson(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            
            return content;
        }
       
    }
}
