using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ColorPanelLoading : MonoBehaviour
{
    [SerializeField] private Image imagePanel;
    public float speedAnim = 1;
    private void OnEnable() {
        
        if(imagePanel == null){
            Debug.Log("imagePanel is null in ColorPanelLoading");
        }
        else{
            ColorLoop();
        }
    }
    // Start is called before the first frame update
    public void ColorLoop()
    {
        if(imagePanel == null) return;
        float value = 2;
        imagePanel.DOColor(Color.red, value).SetDelay(0.2f * speedAnim);
        imagePanel.DOColor(Color.yellow, value).SetDelay(0.4f * speedAnim);
        imagePanel.DOColor(Color.green, value).SetDelay(0.6f * speedAnim);
        imagePanel.DOColor(Color.cyan, value).SetDelay(0.8f * speedAnim);
        imagePanel.DOColor(Color.blue, value).SetDelay(0.10f * speedAnim);
        imagePanel.DOColor(Color.magenta, value).SetDelay(0.12f * speedAnim).OnComplete(ColorLoop);

    }
}
