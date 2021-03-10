using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class Pulse : MonoBehaviour
{
   public  float                                        max  = 1.0f;
   public  float                                        min  = 0.8f;
   public  float                                        time = 1f;
   public bool delayStart;
   public float delayTime;
   private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

   private void OnEnable()
   {
      if(delayStart){
         StartCoroutine(DelayCoroutine(delayTime));
         
      }
      else{
         Down();
      }

   }
   IEnumerator DelayCoroutine(float delay)
    {
       
      yield return new WaitForSeconds(delay);
      Down();
        
    }
   private void OnDisable()
   {
      transform.localScale = Vector3.one * max;
      _tween.Kill();
   }

   private void Up()
   {
      _tween = transform.DOScale(max, time).OnComplete(Down);
   }

   private void Down()
   {
      _tween = transform.DOScale(min, time).OnComplete(Up);
   }
}