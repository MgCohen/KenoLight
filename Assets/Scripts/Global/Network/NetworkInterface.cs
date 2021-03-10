using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class NetworkInterface : MonoBehaviour
{
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


  private readonly       Api     _api    = new Api("https://slots.gamesdasorte.com/api/v1");
  [NonSerialized] public Sorteio SorteioAtual = null;


  public void RequestSorteio(string id, bool offline = false, Action<Sorteio> callback = null)
  {
    void OfflineLoad(string err = null)
    {
      if(err != null) Debug.LogError(err);
      
      Debug.LogWarning("Sorteio Local ");
      var sorteioData = Resources.Load<TextAsset>("Sorteio").text;
      if (sorteioData == null) throw new Exception("Invalid File Path: Sorteio");
      var sorteio = JsonConvert.DeserializeObject<Sorteio>(sorteioData);
      callback?.Invoke(sorteio);
      // UpdateSorteio();
    }

    if (offline)
    {
      OfflineLoad();
      return;
    }
    
    Debug.Log($"Getting Sorteio Nº{id}");
    _api.Get<Sorteio>($"/sorteio/1/{id}/")
        .OnComplete(callback);
  }
   public void RequestLogoOngOferecimento(string sala)
  {
    _api.Get<List<Logo>>($"/logosespecial/{sala}")
       .OnComplete(UpdateLogos)
       .OnError((err) =>
        {
          // Debug.Log("Logos Offline");
          var data = Resources.Load<TextAsset>("LogosEspecial").text;
          if (data == null) throw new Exception("Invalid File Path: Logos");
          UpdateLogos(JsonConvert.DeserializeObject<List<Logo>>(data));
        });
  }
   private void UpdateLogos(List<Logo> logoE)
  {
    foreach(var logo in logoE){
      logo.se = false;
    }
    //events.LogoUrl.Invoke(logoE);
  }

}
