using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class NetworkInterface : MonoBehaviour
{
  public EventHandler events;
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
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.O)) RequestLogoOngOferecimento("sala1");
  }


  private readonly Api _api = new Api("https://slots.gamesdasorte.com");
  [NonSerialized] public Sorteio SorteioAtual = null;

  private void OfflineLoad(string sorteioPath = "", Action<Sorteio> callback = null)
  {



    using (var sr = new StreamReader(sorteioPath))
    using (JsonReader reader = new JsonTextReader(sr))
    {
      var serializer = new JsonSerializer();
      var sorteio = serializer.Deserialize<Sorteio>(reader);

      callback?.Invoke(sorteio);
      return;
    }


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

    Debug.Log($"Getting Sorteio Nº{id}");
    _api.Get<Sorteio>("/static/temp/sorteio.json")
        .OnComplete(callback);
  }
  public void RequestLogoOngOferecimento(string sala)
  {
    _api.Get<List<Logo>>($"/logosongoferecimento/{sala}")
       .OnComplete(UpdateRequestLogoOngOferecimento)
       .OnError((err) =>
        {
          // Debug.Log("Logos Offline");
          var data = Resources.Load<TextAsset>("LogoOngOferecimento").text;
          if (data == null) throw new Exception("Invalid File Path: LogoOngOferecimento");
          UpdateRequestLogoOngOferecimento(JsonConvert.DeserializeObject<List<Logo>>(data));
        });
  }
  public void RequestLogoEspecial(string sala)
  {
    _api.Get<List<Logo>>($"/logosespecial/{sala}")
       .OnComplete(UpdateLogosE)
       .OnError((err) =>
        {
          // Debug.Log("Logos Offline");
          var data = Resources.Load<TextAsset>("LogosEspecial").text;
          if (data == null) throw new Exception("Invalid File Path: Logos");
          UpdateLogosE(JsonConvert.DeserializeObject<List<Logo>>(data));
        });
  }
  public void RequestLogosSuperEspecial(string sala)
  {
    _api.Get<List<Logo>>($"/logosse/{sala}")
       .OnComplete(UpdateLogosSe)
       .OnError((err) =>
        {
          // Debug.Log("Logos Offline");
          var data = Resources.Load<TextAsset>("LogosSuperEspecial").text;
          if (data == null) throw new Exception("Invalid File Path: LogosSuperEspecial");
          UpdateLogosSe(JsonConvert.DeserializeObject<List<Logo>>(data));

        });
  }
  private void UpdateRequestLogoOngOferecimento(List<Logo> logos)
  {
    foreach (var logo in logos)
    {
      logo.se = 1;
    }
    events.LogoUrlOngOferecimento.Invoke(logos);
    Debug.Log("Entrei request");
  }
  private void UpdateLogosE(List<Logo> logos)
  {
    foreach (var logo in logos)
    {
      logo.se = 2;
    }
    events.LogoUrlEspecial.Invoke(logos);
  }
  private void UpdateLogosSe(List<Logo> logos)
  {
    foreach (var logo in logos)
    {
      logo.se = 3;
    }
    events.LogoUrlSuperEspecial.Invoke(logos);
  }

}
