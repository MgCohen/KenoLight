
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PrizeShowcase : MonoBehaviour
{
    public Pulse pulse;
    public Image layer;

    public bool fadeOnStart;

    private void OnEnable()
    {
        if (fadeOnStart)
            layer.DOFade(0.9f, 2f);
    }

    public void Set(bool state)
    {
        pulse.enabled = state;
        layer.gameObject.SetActive(!state);
    }
}
