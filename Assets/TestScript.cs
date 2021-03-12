using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Next), 5f);
    }
    public void Next()
    {
        Destroy(DOTween.instance.gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
