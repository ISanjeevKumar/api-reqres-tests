using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace In.Reqres.Tests.WebServices
{
    public class RequestClient : HttpClient
    {
        private readonly string _environmentUrl;
        private readonly string _mediaType;

        public RequestClient(string environmentUrl, string mediaType = "application/json")
        {
            _environmentUrl = environmentUrl;
            _mediaType = mediaType;
        }

        public HttpRequestMessage CreateRequestMessage(string urlParams, HttpMethod httpMethod, HttpContent httpContent, List<KeyValuePair<string, string>> requestHeaders)
        {
            var requestMessage = new HttpRequestMessage(httpMethod, _environmentUrl + urlParams)
            {
                Content = httpContent
            };

            if (requestHeaders != null)
            {
                foreach (var header in requestHeaders)
                {
                    requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            return requestMessage;
        }

        public HttpResponseMessage Get(string urlParams, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            var requestMessage = CreateRequestMessage(urlParams, HttpMethod.Get, httpContent: null, requestHeaders);
            return SendAsync(requestMessage).Result;
        }

        public TResponse Get<TResponse>(string urlParams, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            var requestMessage = CreateRequestMessage(urlParams, HttpMethod.Get, httpContent: null, requestHeaders);
            var response = SendAsync(requestMessage).Result;
            return DeserializeResponse<TResponse>(response);
        }

        public HttpResponseMessage Post<TData>(TData data, string urlParams, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            HttpContent content = SerializeRequest(data);
            var requestMessage = CreateRequestMessage(urlParams, HttpMethod.Post, httpContent: content, requestHeaders);
            return SendAsync(requestMessage).Result;
        }

        public TResponse Post<TResponse, TData>(TData data, string urlParams, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            HttpContent content = SerializeRequest(data);
            var requestMessage = CreateRequestMessage(urlParams, HttpMethod.Post, httpContent: content, requestHeaders);
            var response = SendAsync(requestMessage).Result;
            return DeserializeResponse<TResponse>(response);
        }

        #region Private Methods
        private TResponse DeserializeResponse<TResponse>(object responseData)
        {
            object data = (responseData as HttpResponseMessage).Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TResponse>(data.ToString());
        }

        private HttpContent SerializeRequest<TData>(TData data)
        {
            HttpContent content = null;

            if (typeof(TData) == typeof(string))
                content = new StringContent(data.ToString(), encoding: Encoding.UTF8, mediaType: _mediaType);
            else
                content = new StringContent(JsonConvert.SerializeObject(data), encoding: Encoding.UTF8, mediaType: _mediaType);
            return content;
        }

        #endregion

    }
}
