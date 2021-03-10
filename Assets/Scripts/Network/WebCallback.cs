using System;
using UnityEngine;

public class WebCallback<T>
{
   private          Action<T>          _completeCallback;
   private          Action<string>     _errorCallback;
   public           WebCallbackHandler Handler { get; }

   public void DispatchComplete(T data)
   {
      _completeCallback?.Invoke(data);
   }

   public void DispatchError(string error)
   {
      if (_errorCallback != null)
      {

         _errorCallback.Invoke(error);
      }
      else
      {
         Debug.LogError(error);
      }
   }

   public WebCallback()
   {
      Handler = new WebCallbackHandler(this);
   }

   public class WebCallbackHandler
   {
      private WebCallback<T> _parent;

      internal WebCallbackHandler(WebCallback<T> parent)
      {
         _parent = parent;
      }

      public WebCallbackHandler OnComplete(Action action)
      {
         return OnComplete((_) => action());
      }

      public WebCallbackHandler OnComplete(Action<T> action)
      {
         _parent._completeCallback = action;
         return this;
      }

      public WebCallbackHandler OnError(Action action)
      {
         return OnError((_) => action());
      }

      public WebCallbackHandler OnError(Action<string> action)
      {
         _parent._errorCallback = action;
         return this;
      }
   }
}