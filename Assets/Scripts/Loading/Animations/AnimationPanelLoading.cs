using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationPanelLoading : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] private GameObject panel;
  [SerializeField] private GameObject panelText;
  [SerializeField] private GameObject panelMove;
  [SerializeField] private float valorTransform = 20;
  [SerializeField] private float sizePanel = 1.0f;
   [SerializeField] private float sizeText = 1.0f;
  void OnEnable()
  {
    
    if(panel != null){  
      panel.transform.localScale = new Vector3(0.001f,0.001f,0.001f);
      AnimScale(); 
    } 
   
    if(panelText != null){ 
      panelText.transform.localScale = new Vector3(0.001f,0.001f,0.001f);
      TextScale();
   }
   if(panelMove != null){
     AnimLoop2();
   }
   
    //imagePanel = panel.GetComponent<Image>();
  }
   public void TextScale()
  {
    if(panelText == null) return;
    panelText.transform.DOScale(sizeText, 0.5f).SetEase(Ease.Linear);
    panelText.transform.DOLocalMoveY(-40f,0.5f);
  }
  public void AnimScale()
  {
    if(panel == null) return;
    panel.transform.DOScale(sizePanel , 0.5f).SetEase(Ease.Linear).OnComplete(AnimLoop2);
  }
  
  public void AnimLoop2()
  {
    if(panelMove == null) return;
    panelMove.transform.DORotate(new Vector3(valorTransform, -valorTransform, 0), 5).SetEase(Ease.Linear).OnComplete(AnimLoop3);
  }
  public void AnimLoop3()
  {
    if(panelMove == null) return;
    panelMove.transform.DORotate(new Vector3(-valorTransform, valorTransform, 0), 5).SetEase(Ease.Linear).OnComplete(AnimLoop2);
  }
 

}
