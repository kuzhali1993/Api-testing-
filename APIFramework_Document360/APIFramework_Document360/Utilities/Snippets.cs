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
    class Snippets
    {
        public static void AssertResponseCode(HttpResponseMessage response,int statuscode)
        {
            int codenum = (int)response.StatusCode;
                Assert.AreEqual(statuscode,codenum,"Expected status code is not equal to actual code");
          
            
        }

        public static void CheckJsonValue(HttpResponseMessage response,string xpath,string value)
        {
            if(response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var Jsoncontent = JObject.Parse(content);
                var token =Jsoncontent.SelectToken("categories[0].title");
                Assert.AreEqual(value, token.ToString(),"path value is not same");
                //Console.WriteLine(token.ToString());
            }
            else
            {
                Assert.Fail("Response json content is invalid");
            }
        }

       private static dynamic Getjson(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            
            return content;
        }
       
    }
}
