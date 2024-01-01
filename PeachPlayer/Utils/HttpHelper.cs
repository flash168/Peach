using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace PeachPlayer.Utils
{
    public class HttpResult<T>
    {
        public HttpResult(string errormage)
        {
            Message = errormage;
            IsSuccessful = false;
        }
        public HttpResult() { }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public HttpStatusCode Code { get; set; }
        public T Content { get; set; }
    }


    //https://120.79.244.92/api.php/provide/vod/?ac=detail
    public class HttpHelper
    {

        /// <summary>
        /// GET获取接口数据
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="UrlSegments">请求地址栏数据，问号前，斜杠隔开（可空）</param>
        /// <param name="QueryString">请求地址栏参数 问号后，XXX=XX（可空）</param>
        /// <param name="Body">提交数据</param>
        /// <returns></returns>
        public Task<HttpResult<T>> GetMessageAsy<T>(string url, Dictionary<string, string> UrlSegments = null, Dictionary<string, string> QueryString = null, object Body = null)
        {
            return Request<T>(url, Method.Get, UrlSegments, QueryString, Body);
        }


        /// <summary>
        /// POST请求服务
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="Body">提交数据</param>
        /// <param name="UrlSegments">请求地址栏数据，问号前，斜杠隔开（可空）</param>
        /// <param name="QueryString">请求地址栏参数 问号后，XXX=XX（可空）</param>
        /// <param name="timeout">请求超时（可空）</param>
        /// <returns></returns>
        public Task<HttpResult<T>> PostMessageAsy<T>(string url, object Body, Dictionary<string, string> UrlSegments = null, Dictionary<string, string> QueryString = null)
        {
            return Request<T>(url, Method.Post, UrlSegments, QueryString, Body);
        }

        //string BaseUrl { get; set; } = "http://120.79.244.92";
        string BaseUrl = "https://lbapi9.com";
        //public string BaseUrl = "https://www.kuaibozy.com";

        private async Task<HttpResult<T>> Request<T>(string url, Method method, Dictionary<string, string> UrlSegments, Dictionary<string, string> QueryString, object Body)
        {
            HttpResult<T> req = new HttpResult<T>();
            try
            {
                if (url.Contains("http"))
                    BaseUrl = url.Substring(0, url.IndexOf('/', 12));
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(url, method);
                request.Timeout = 10000;
                request.RequestFormat = DataFormat.Json;
                if (UrlSegments != null)
                {
                    foreach (var item in UrlSegments)
                    {
                        request.AddUrlSegment(item.Key, item.Value.Trim());
                    }
                }

                if (QueryString != null)
                {
                    foreach (var item in QueryString)
                    {
                        request.AddQueryParameter(item.Key, item.Value.Trim());
                    }
                }

                if (Body != null)
                {
                    //string json = JsonConvert.SerializeObject(Body, Formatting.Indented);
                    request.AddParameter("application/json", Body, ParameterType.RequestBody);
                }

                var restResponse = await client.ExecuteAsync<T>(request);
                req.Code = restResponse.StatusCode;
                Debug.WriteLine($"---获取{url}：{restResponse.StatusCode}");
                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    req.IsSuccessful = true;
                    if (typeof(T).Name is "String" || typeof(T).Name is "string")
                    {
                        req.Content = (T)Convert.ChangeType(restResponse.Content, typeof(T));
                    }
                    else
                    {
                        req.Content = JsonConvert.DeserializeObject<T>(restResponse.Content);
                    }

                    return req;
                }
                else if (restResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    req.Message = "@@服务器暂时不可用，通常是由于过多加载或维护!";
                    return req;
                }
                else
                {
                    string data = restResponse.StatusCode.ToString();
                    if (data == null) { data = restResponse.ErrorMessage; }
                    req.Message = "@@" + data;
                    return req;
                }
            }
            catch (Exception e)
            {
                // LogService.Instance.Writer(e.Message);
                throw e;
            }
        }



    }
}
