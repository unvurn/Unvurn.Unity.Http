using System;
using System.Collections.Generic;
using Unvurn.Core;
using UnityEngine.Networking;
using Unvurn.Unity.Http.Internal;

namespace Unvurn.Unity.Http
{
    public static class HttpClientCoroutineExtensions
    {
        public static void Get(this HttpClient self, string url, Action<UnityWebRequest> finished)
        {
            HttpRequestProcessor.Process(MakeCoroutine(self.CreateGetRequest<object>(url, null)).Then(asyncOp => finished(asyncOp.webRequest)));
        }

        public static void Get<T>(this HttpClient self, string url, T parameters, Action<UnityWebRequest> finished)
        {
            HttpRequestProcessor.Process(MakeCoroutine(self.CreateGetRequest(url, parameters)).Then(asyncOp => finished(asyncOp.webRequest)));
        }

        public static void Post(this HttpClient self, string url, Action<UnityWebRequest> finished)
        {
            HttpRequestProcessor.Process(MakeCoroutine(self.CreatePostRequest<object>(url, null)).Then(asyncOp => finished(asyncOp.webRequest)));
        }

        public static void Post<T>(this HttpClient self, string url, T parameters, Action<UnityWebRequest> finished)
        {
            HttpRequestProcessor.Process(MakeCoroutine(self.CreatePostRequest(url, parameters)).Then(asyncOp => finished(asyncOp.webRequest)));
        }

        public static void Put(this HttpClient self, string url, IDictionary<string, string> parameters, Action<UnityWebRequest> finished)
        {
            HttpRequestProcessor.Process(MakeCoroutine(self.CreatePutRequest(url, parameters)).Then(asyncOp => finished(asyncOp.webRequest)));
        }

        private static IEnumerator<UnityWebRequestAsyncOperation> MakeCoroutine(UnityWebRequest request)
        {
            yield return request.SendWebRequest();
        }
    }
}