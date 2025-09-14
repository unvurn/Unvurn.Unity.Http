using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Unvurn.Unity.Http.Async
{
    /// <summary>
    ///   HttpClientに対してTaskによる非同期機能を追加するための拡張メソッド群。
    /// </summary>
    public static class HttpClientAsyncTaskExtensions
    {
        public static Task<UnityWebRequest> GetAsync(this HttpClient self, string url)
        {
            // dummy
            return self.DoSendWebRequestAsync(self.CreateGetRequest<object>(url, null));
        }

        public static Task<UnityWebRequest> GetAsync<T>(this HttpClient self, string url, T parameters)
        {
            return self.DoSendWebRequestAsync(self.CreateGetRequest(url, parameters));
        }

        public static Task<UnityWebRequest> PostAsync<T>(this HttpClient self, string url, T parameters)
        {
            return self.DoSendWebRequestAsync(self.CreatePostRequest(url, parameters));
        }

#if false
        [Obsolete]
        public static Task<UnityWebRequest> PostFileAsync(this HttpClient self, string url, IDictionary<string, string> parameters, string filePath, string fileKey)
        {
            return self.DoOperationAsync(self.CreatePostFileRequest(url, parameters, filePath, fileKey));
        }
#endif

        public static Task<UnityWebRequest> PutAsync(this HttpClient self, string url, IDictionary<string, string> parameters)
        {
            return self.DoSendWebRequestAsync(self.CreatePutRequest(url, parameters));
        }

        private static async Task<UnityWebRequest> DoSendWebRequestAsync(this HttpClient _, UnityWebRequest request)
        {
            return await request.SendWebRequest();
        }
    }
}
