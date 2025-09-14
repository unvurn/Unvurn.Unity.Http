using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Unvurn.Unity.Http.Internal
{
    [AddComponentMenu("")]
    internal class HttpRequestProcessor : SingletonizedMonoBehaviour<HttpRequestProcessor>
    {
        internal static void Process(IEnumerator<UnityWebRequestAsyncOperation> request)
        {
            Instance.StartCoroutine(request);
        }
    }
}
