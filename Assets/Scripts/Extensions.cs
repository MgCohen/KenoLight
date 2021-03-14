using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Extensions
{
   public static int IndexOf(this int[] self,int target ,int maxValue)
   {
      for (var i = 0; i < maxValue + 1; ++i)
      {
         if (self[i] == target) return i;
      }
      return -1;
   }
    public static IEnumerator DownloadImage(string MediaUrl, Action<Sprite> callback, Action<String> Error = null)
   {
      var manager = ImageCacheManager.Instance;

      if (manager.Sprites.ContainsKey(MediaUrl))
      {
         callback(manager.Sprites[MediaUrl]);
         yield break;
      }

      UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
      yield return request.SendWebRequest();
      if (request.isNetworkError || request.isHttpError)
         Error?.Invoke(request.error);
      else
      {
         var tex    = ((DownloadHandlerTexture)request.downloadHandler).texture;
         var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
         callback(sprite);
      }
   }
}
