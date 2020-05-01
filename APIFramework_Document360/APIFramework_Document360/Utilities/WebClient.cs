using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.Utilities
{
    static class WebClient
    {
        public static HttpResponseMessage GetResponse(string url)
        {
            
            
                HttpClient httpclient = new HttpClient();
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                Task<HttpResponseMessage> response = httpclient.SendAsync(requestMessage);
                
                HttpResponseMessage responseMessage = response.Result;
                return responseMessage;
                
            
            
        }

        public static HttpResponseMessage GetResponse(string url, Dictionary<string, string> Headers)
        {
            HttpClient httpclient = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            foreach (KeyValuePair<string, string> header in Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);

            }
            Task<HttpResponseMessage> response = httpclient.SendAsync(requestMessage);
            HttpResponseMessage responseMessage = response.Result;
            return responseMessage;
        }

        public static HttpResponseMessage PostRequest(string url, string payload, Dictionary<string, string> Headers)
        {
            HttpClient httpclient = new HttpClient();
            // Uri requestUri = new Uri(url);
            // HttpContent requestcontent = new StringContent(payload, Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            foreach (KeyValuePair<string, string> header in Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);

            }
            Task<HttpResponseMessage> response = httpclient.SendAsync(requestMessage);
            HttpResponseMessage responseMessage = response.Result;
            return responseMessage;

        }

    }
}
