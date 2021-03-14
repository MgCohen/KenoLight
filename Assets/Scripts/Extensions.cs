using System;
using System.Collections;
using UnityEngine;

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
    public static IEnumerator DownloadImage(string mediaUrl, Action<Sprite> callback, Action<string> error = null)
   {
      var manager = ImageCacheManager.Instance;

      if (manager.Sprites.ContainsKey(mediaUrl))
      {
         callback(manager.Sprites[mediaUrl]);
         yield break;
      }

      // UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
      // yield return request.SendWebRequest();
      // if (request.isNetworkError || request.isHttpError)
      //    Error?.Invoke(request.error);
      // else
      // {
      //    var tex    = ((DownloadHandlerTexture)request.downloadHandler).texture;
      //    var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
      //    callback(sprite);
      // }
   }
}
