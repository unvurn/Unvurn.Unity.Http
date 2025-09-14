using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unvurn.Network.Http;
// ReSharper disable MemberCanBeMadeStatic.Global

namespace Unvurn.Unity.Http
{
    public class HttpClient
    {
        private HttpClient()
        {
        }

        public UnityWebRequest CreateGetRequest<T>(string url, T? parameters)
        {
            var endPoint = url + (parameters != null ? "?" + QueryStringPackager.Pack(parameters) : string.Empty);
            return UnityWebRequest.Get(endPoint);
        }

        public UnityWebRequest CreatePostRequest<T>(string url, T? parameters)
        {
            var formData = FormDataPackager.Pack(parameters);
            return UnityWebRequest.Post(url, formData);
        }

        public UnityWebRequest CreatePutRequest(string url, IDictionary<string, string> parameters)
        {
            var body = JsonPackager.Pack(parameters);
            var request = UnityWebRequest.Put(url, body);
            request.SetRequestHeader("content-type", "application/json");
            return request;
        }

        public static HttpClient Create()
        {
            return new HttpClient();
        }

        private static readonly IHttpContentPackager<string> QueryStringPackager = new QueryStringPackager();
        private static readonly IHttpContentPackager<WWWForm> FormDataPackager = new FormDataPackager();
        private static readonly IHttpContentPackager<byte[]> JsonPackager = new JsonPackager();
    }
}
