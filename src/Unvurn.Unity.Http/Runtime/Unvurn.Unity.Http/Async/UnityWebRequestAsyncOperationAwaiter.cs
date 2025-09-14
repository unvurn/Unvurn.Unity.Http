using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace Unvurn.Unity.Http.Async
{
    public class UnityWebRequestAsyncOperationAwaiter : INotifyCompletion
    {
        public UnityWebRequestAsyncOperationAwaiter(UnityWebRequestAsyncOperation asyncOperation)
        {
            _asyncOperation = asyncOperation;
            asyncOperation.completed += OnRequestCompleted;
        }

        public bool IsCompleted => _asyncOperation.isDone;

        public UnityWebRequest GetResult()
        {
            return _asyncOperation.webRequest;
        }

        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
        }

        private void OnRequestCompleted(AsyncOperation obj)
        {
            _continuation();
        }

        private readonly UnityWebRequestAsyncOperation _asyncOperation;
        private Action _continuation = () => { };
    }

    public static class UnityWebRequestAsyncOperationExtensionMethods
    {
        public static UnityWebRequestAsyncOperationAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
        {
            return new UnityWebRequestAsyncOperationAwaiter(asyncOp);
        }
    }
}
