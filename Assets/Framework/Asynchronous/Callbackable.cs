using System;

namespace GameCore.Framework.Asynchronous
{
    public interface ICallbackable
    {
        void OnCallback(Action<IAsyncResult> callback);
    }
    
    public interface ICallbackable<TResult>
    {
        void OnCallback(Action<IAsyncResult<TResult>> callback);
    }
}