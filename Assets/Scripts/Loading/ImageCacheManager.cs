using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Events;

public class ImageCacheManager : MonoBehaviour
{
  public static readonly UnityEvent<List<Logo>> ONLogoLoad = new UnityEvent<List<Logo>>();
  public readonly Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();

  // public readonly Dictionary<string, List<UniGif.GifTexture>> Gifs =
  //    new Dictionary<string, List<UniGif.GifTexture>>();

  private static ImageCacheManager _instance;

  public static ImageCacheManager Instance
  {
    get
    {
      if (_instance != null) return _instance;

      var obj = new GameObject("AdManager");
      _instance = obj.AddComponent<ImageCacheManager>();
      Debug.LogWarning("Couldn't find admanager");
      return _instance;
    }
  }


  private EventHandler _handler;

  private EventHandler Handler => !_handler
                                     ? _handler = Resources.Load<EventHandler>("Events")
                                     : _handler;

  private void Awake()
  {
    _instance = this;
  }

  private void OnEnable()
  {
    Handler.LogoUrlOngOferecimento.AddListener(StartLoadCoroutine);
    Handler.LogoUrlEspecial.AddListener(StartLoadCoroutine);
    Handler.LogoUrlSuperEspecial.AddListener(StartLoadCoroutine);

  }

  private void OnDisable()
  {
    Handler.LogoUrlOngOferecimento.RemoveListener(StartLoadCoroutine);
    Handler.LogoUrlEspecial.RemoveListener(StartLoadCoroutine);
    Handler.LogoUrlSuperEspecial.RemoveListener(StartLoadCoroutine);
  }


  #region loadAd

  private void StartLoadCoroutine(List<Logo> logo)
  {

    StartCoroutine(LoadImages(logo));
    Debug.Log("Entrei guardar imagem logo");
  }

  private IEnumerator LoadImages(List<Logo> logo)
  {
    Debug.Log("Carreguei imagens");
    foreach (var url in logo)
    {
      //Debug.Log($"loading {url.id}.png");
      yield return LoadInMemory(url);
    }
    

    Debug.Log("Loaded");
    ONLogoLoad.Invoke(logo);
    //Handler.OnAdLoad.Invoke();
    //evento
  }

  private IEnumerator LoadInMemory(Logo logo)
  {
    var loading = true;


    var fileName = logo.id + ".png";


    // Debug.Log(fileName);
    Sprite sprite = null;

    if (IsInCache(fileName))
    {
      // Debug.Log("In Cache");
      yield return GetFromCache(fileName, (s) =>
      {
        // Debug.Log($"Got From Cache {fileName}");
        sprite = s;
        loading = false;
      });
    }
    else
    {

      UnityWebRequest request = UnityWebRequestTexture.GetTexture(logo.url);
      yield return request.SendWebRequest();

      if (request.result != UnityWebRequest.Result.ConnectionError)
      {
        SaveOnCache(request.downloadHandler.data, fileName);
        var tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
        sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
      }

      loading = false;
    }

    while (loading)
    {
      yield return null;
    }

    if (sprite == null) yield break;

    if (!Sprites.ContainsKey(fileName)) Sprites.Add(fileName, sprite);
    else Sprites[fileName] = sprite;





  }

  private bool IsInCache(string fileName)
  {
    var filePath = Path.Combine(Application.persistentDataPath, "cache", fileName);
    return File.Exists(filePath);
  }
  private IEnumerator GetFromCache(string fileName, Action<Sprite> callback, Action err = null)
  {
    var filePath = Path.Combine(Application.persistentDataPath, "cache", fileName);

    if (!IsInCache(filePath))
    {
      Debug.Log("Errr");
      err?.Invoke();
      yield break;
    }

    var loading = true;
    var requestUri = "file:///" + filePath;
    // Debug.Log(requestUri);
    yield return Extensions.DownloadImage(requestUri, (sprite) =>
    {

      //convertendo para int
      if (!Sprites.ContainsKey(fileName)) Sprites.Add(fileName, sprite);
      loading = false;
      callback(sprite);
    });

    while (loading)
    {
      yield return null;
    }
  }
  private void SaveOnCache(Byte[] bytes, string fileName)
  {
    var filePath = Path.Combine(Application.persistentDataPath, "cache", fileName);

    if (File.Exists(filePath)) return;
    var file = new FileInfo(filePath);
    file.Directory?.Create();
    File.WriteAllBytes(filePath, bytes);
  }




  #endregion
}