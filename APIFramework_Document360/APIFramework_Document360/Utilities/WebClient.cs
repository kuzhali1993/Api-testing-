using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Newtonsoft.Json;
using System.Diagnostics;


namespace APIFramework_Document360.Utilities
{
    public static class WebClient
    {
        
        public static HttpResponseMessage PostRequest(string url)
        {
            
            
                HttpClient httpclient = new HttpClient();
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                
            try
            {
                Task<HttpResponseMessage> response = httpclient.SendAsync(requestMessage);
                HttpResponseMessage responseMessage = response.Result;
                return responseMessage;
            }
            finally
            {
                httpclient.Dispose();

            }
        }

        public static HttpResponseMessage GetResponse(string url, Dictionary<string, string> Headers)
        {
            HttpClient httpclient = new HttpClient();
            var watch = new Stopwatch();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            foreach (KeyValuePair<string, string> header in Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);

            }

            
          
            try
            {
                watch.Start();
                Task<HttpResponseMessage> response = httpclient.SendAsync(requestMessage);
                watch.Stop();
                HttpResponseMessage responseMessage = response.Result;
                Reporter.LogDetails(responseMessage.RequestMessage.RequestUri.ToString());
                var Jsoncontent = responseMessage.Content.ReadAsStringAsync().Result;
               
               // Console.WriteLine(responsetime);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var str = JObject.Parse(Jsoncontent);
                    Reporter.testcaseLogger.Log(Status.Info, MarkupHelper.CreateCodeBlock(JsonConvert.SerializeObject(str, Formatting.Indented)));
                }
                else
                {
                    Reporter.testcaseLogger.Log(Status.Info, MarkupHelper.CreateCodeBlock(JsonConvert.SerializeObject(Jsoncontent, Formatting.Indented)));
                }
                return responseMessage;
                
            }
            finally
            {
                httpclient.Dispose();

            }
        }

        public static HttpResponseMessage PutRequest(string url, Dictionary<string, string> Headers,string payload )
        {
            HttpClient httpclient = new HttpClient();
            var watch = new Stopwatch();
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            foreach (KeyValuePair<string, string> header in Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);

            }
            try
            {
                Task<HttpResponseMessage> response = httpclient.SendAsync(requestMessage);
                HttpResponseMessage responseMessage = response.Result;
                Reporter.LogDetails(responseMessage.RequestMessage.RequestUri.ToString());
                var Jsoncontent = responseMessage.Content.ReadAsStringAsync().Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var str = JObject.Parse(Jsoncontent);
                    Reporter.testcaseLogger.Log(Status.Info, MarkupHelper.CreateCodeBlock(JsonConvert.SerializeObject(str, Formatting.Indented)));
                }
                else
                {
                    Reporter.testcaseLogger.Log(Status.Info, MarkupHelper.CreateCodeBlock(JsonConvert.SerializeObject(Jsoncontent, Formatting.Indented)));
                }

                return responseMessage;
            }
            finally
            {
                httpclient.Dispose();

            }

        }

        public static HttpResponseMessage PostRequest(string url, Dictionary<string, string> Headers, string payload)
        {
            HttpClient httpclient = new HttpClient();
            var watch = new Stopwatch();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            foreach (KeyValuePair<string, string> header in Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);

            }
            try
            {
                watch.Start();
                Task<HttpResponseMessage> response = httpclient.SendAsync(requestMessage);
                
                HttpResponseMessage responseMessage = response.Result;
                watch.Stop();
                Reporter.LogDetails(responseMessage.RequestMessage.RequestUri.ToString());
                var Jsoncontent = responseMessage.Content.ReadAsStringAsync().Result;
                var responsetime = watch.ElapsedMilliseconds;
                //Console.WriteLine(responsetime);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var str = JObject.Parse(Jsoncontent);
                    Reporter.testcaseLogger.Log(Status.Info, MarkupHelper.CreateCodeBlock(JsonConvert.SerializeObject(str, Formatting.Indented)));
                }
                else
                {
                    Reporter.testcaseLogger.Log(Status.Info, MarkupHelper.CreateCodeBlock(JsonConvert.SerializeObject(Jsoncontent, Formatting.Indented)));
                }


                return responseMessage;
            }
            finally
            {
                httpclient.Dispose();

            }

        }

        public static HttpResponseMessage DeleteRequest(string url, Dictionary<string, string> Headers)
        {
            HttpClient httpclient = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            foreach (KeyValuePair<string, string> header in Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);

            }

            try
            {
                Task<HttpResponseMessage> response = httpclient.SendAsync(requestMessage);
                HttpResponseMessage responseMessage = response.Result;
                Reporter.LogDetails(responseMessage.RequestMessage.RequestUri.ToString());
                var Jsoncontent = responseMessage.Content.ReadAsStringAsync().Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var str = JObject.Parse(Jsoncontent);
                    Reporter.testcaseLogger.Log(Status.Info, MarkupHelper.CreateCodeBlock(JsonConvert.SerializeObject(str, Formatting.Indented)));
                }
                else
                {
                    Reporter.testcaseLogger.Log(Status.Info, MarkupHelper.CreateCodeBlock(JsonConvert.SerializeObject(Jsoncontent, Formatting.Indented)));
                }
                return responseMessage;

            }
            finally
            {
                httpclient.Dispose();

            }
        }


    }
}
