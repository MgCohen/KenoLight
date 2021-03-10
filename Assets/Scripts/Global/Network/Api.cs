using System.Diagnostics;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

public class Api
{
  private string _token = "";
  private readonly string _endpoint;

  public Api(string endpoint = "http://localhost:3000")
  {
    this._endpoint = endpoint;
  }

  public void SetToken(string token)
  {
    _token = token;
  }

  #region GET

  public WebCallback<byte[]>.WebCallbackHandler GetBytes(string path)
  {

    var request = UnityWebRequest.Get($"{_endpoint}{path}");
    var callback = new WebCallback<byte[]>();

    if (!string.IsNullOrWhiteSpace(_token))
      request.SetRequestHeader("authorization", $"Bearer {_token}");

    request.SendWebRequest().completed += (op) =>
    {
      if (!string.IsNullOrEmpty(request.error))
      {
        callback.DispatchError(request.error);
        return;
      }

      callback.DispatchComplete(request.downloadHandler.data);
    };

    return callback.Handler;
  }

  public WebCallback<string>.WebCallbackHandler Get(string path)
  {
    var fullPath = $"{_endpoint}{path}";
    var request  = UnityWebRequest.Get(fullPath);
    // Debug.Log(fullPath);
    var callback = new WebCallback<string>();

    if (!string.IsNullOrWhiteSpace(_token))
      request.SetRequestHeader("authorization", $"Bearer {_token}");


    request.SendWebRequest().completed += (op) =>
    {
      if (!string.IsNullOrEmpty(request.error))
      {
        callback.DispatchError(request.error);
        return;
      }
      callback.DispatchComplete(request.downloadHandler.text);
    };

    return callback.Handler;
  }

  public WebCallback<T>.WebCallbackHandler Get<T>(string path)
  {
    var callback = new WebCallback<T>();
    var request = Get(path);
    request.OnComplete((json) =>
    {
      //dados em json recebidos
      // Debug.Log(json);
      var data = JsonConvert.DeserializeObject<T>(json);
      callback.DispatchComplete(data);
    });

    request.OnError(callback.DispatchError);

    return callback.Handler;
  }

  #endregion

  #region DELETE


  //o que é delete?
  public WebCallback<string>.WebCallbackHandler Delete(string path)
  {
    var request = UnityWebRequest.Delete($"{_endpoint}{path}");
    var callback = new WebCallback<string>();

    if (!string.IsNullOrWhiteSpace(_token))
      request.SetRequestHeader("authorization", $"Bearer {_token}");

    request.SendWebRequest().completed += (op) =>
    {
      if (!string.IsNullOrEmpty(request.error))
      {
        callback.DispatchError(request.error);
        return;
      }

      callback.DispatchComplete(request.downloadHandler.text);
    };


    return callback.Handler;
  }



  public WebCallback<T>.WebCallbackHandler Delete<T>(string path)
  {
    var callback = new WebCallback<T>();
    var request = Delete(path);

    request.OnComplete((json) =>
    {
      var data = JsonConvert.DeserializeObject<T>(json);
      callback.DispatchComplete(data);
    });

    request.OnError(callback.DispatchError);

    return callback.Handler;
  }

  #endregion

  #region PAYLOAD

  protected WebCallback<string>.WebCallbackHandler BasePayload(string method, string path, string payload)
  {
    var encodedPayload = new System.Text.UTF8Encoding().GetBytes(payload);
    var request = new UnityWebRequest($"{_endpoint}{path}", method);
    var callback = new WebCallback<string>();

    //Payload
    request.uploadHandler = new UploadHandlerRaw(encodedPayload);
    request.downloadHandler = new DownloadHandlerBuffer();

    //Headess
    request.SetRequestHeader("Content-Type", "application/json");
    request.SetRequestHeader("cache-control", "no-cache");
    if (!string.IsNullOrWhiteSpace(_token)) request.SetRequestHeader("authorization", $"Bearer {_token}");

    request.SendWebRequest().completed += (op) =>
    {
      if (!string.IsNullOrEmpty(request.error))
      {
        callback.DispatchError(request.downloadHandler.text);
        return;
      }

      callback.DispatchComplete(request.downloadHandler.text);
    };

    return callback.Handler;
  }


  #region POST



  public WebCallback<string>.WebCallbackHandler Post(string path, string payload)
  {
    return BasePayload(UnityWebRequest.kHttpVerbPOST, path, payload);
  }

  public WebCallback<TReturn>.WebCallbackHandler Post<TReturn>(string path, object payload)
  {
    var callback = new WebCallback<TReturn>();
    var jsonData = payload is string sPayload ? sPayload : JsonConvert.SerializeObject(payload);
    if (!(payload is string))
      Debug.Log(jsonData);

    var request = Post(path, jsonData);

    request.OnComplete((json) =>
    {
      var data = JsonConvert.DeserializeObject<TReturn>(json);
      callback.DispatchComplete(data);
    });

    request.OnError(callback.DispatchError);

    return callback.Handler;
  }

  public WebCallback<string>.WebCallbackHandler Post<TPayload>(string path, TPayload payload)
  {
    string jsonData = JsonConvert.SerializeObject(payload);
    var request = Post(path, jsonData);

    return request;
  }

  #endregion


  #region PUT

  public WebCallback<string>.WebCallbackHandler Put(string path, string payload)
  {
    return BasePayload(UnityWebRequest.kHttpVerbPUT, path, payload);
  }

  public WebCallback<TReturn>.WebCallbackHandler Put<TPayload, TReturn>(string path, TPayload payload)
     where TPayload : class
  {
    var callback = new WebCallback<TReturn>();
    var jsonData = JsonConvert.SerializeObject(payload);
    var request = Put(path, jsonData);

    request.OnComplete((json) =>
    {
      var data = JsonConvert.DeserializeObject<TReturn>(json);
      callback.DispatchComplete(data);
    });

    request.OnError(callback.DispatchError);

    return callback.Handler;
  }

  public WebCallback<string>.WebCallbackHandler Put<TPayload>(string path, TPayload payload)
  {
    var jsonData = JsonConvert.SerializeObject(payload);
    Debug.Log(jsonData);
    var request = Put(path, jsonData);

    return request;
  }

  #endregion

  #endregion
}