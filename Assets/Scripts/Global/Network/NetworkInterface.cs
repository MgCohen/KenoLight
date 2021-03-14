using System;
using System.IO;
using UnityEngine;
using Utf8Json;

public class NetworkInterface : MonoBehaviour
{
  // public EventHandler events;
  private static NetworkInterface _instance;

  public static NetworkInterface Instance
  {
    get
    {
      if (_instance != null) return _instance;

      var obj = new GameObject("NetworkInteraface");
      _instance = obj.AddComponent<NetworkInterface>();
      Debug.LogWarning("Couldn't find network interface");
      return _instance;
    }
  }

  private void Awake()
  {
    if (_instance != null)
    {
      Destroy(gameObject);
      return;
    }

    _instance = this;
    DontDestroyOnLoad(gameObject);
  }

  private readonly Api _api = new Api("https://slots.gamesdasorte.com");

  private static void OfflineLoad<T>(string sorteioPath = "", Action<T> callback = null)
  {
    using var sourceStream = File.Open(sorteioPath, FileMode.Open);
    var       data         = JsonSerializer.Deserialize<T>(sourceStream);
      
    callback?.Invoke(data);
    GC.Collect();
  }

  public void RequestSorteio(string id, bool offline = false, Action<Sorteio> callback = null)
  {
    
    if (offline)
    {
      var sorteioPath = Application.persistentDataPath + "/sorteio.json";
      Debug.LogWarning("Sorteio Local: " + sorteioPath);
      
      // if (File.Exists(sorteioPath))
      // {
      //   OfflineLoad(sorteioPath, callback);
      //   return;
      // }
      
      Debug.Log("Salvando sorteio na maquina");
      var ogPath = Application.streamingAssetsPath + "/sorteio.json";
      
      new Api("").GetFile(ogPath, sorteioPath)
      .OnComplete(() => OfflineLoad(sorteioPath, callback));

      return;
    }
    
    // var sorteioData = Resources.Load<TextAsset>("Sorteio").text;
    // if (sorteioData == null) throw new Exception("Invalid File Path: Sorteio");
    // // Debug.Log(sorteioData);
    // var sorteio = JsonSerializer.Deserialize<Sorteio>(sorteioData);
    // callback?.Invoke(sorteio);
    // return;

    Debug.Log($"Getting Sorteio Nº{id}");
    _api.Get<Sorteio>("/static/temp/sorteio.json")
        .OnComplete(callback);
  }

  public void RequestNil()
  {
    _api.Get<string>("/static/temp/test.txt")
    .OnComplete(Debug.Log)
    .OnError(Debug.Log);
  }


}
