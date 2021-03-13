
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class TestScript : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public float tempo = 20f;
    private readonly Vector3 rotate = new Vector3(0, 0, -90f);
    private void Start()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        DOTween.KillAll();
        Destroy(DOTween.instance.gameObject);
        counter.text += "\n" + Game.loopCount;
        Invoke(nameof(Next), tempo);
        // InvokeRepeating(nameof(Load1s), 60, 60);
    }
    private void Update()
    {
        counter.transform.Rotate(rotate * Time.deltaTime);
    }

    private void Load1s()
    {
        // networkInterface.RequestNil();
    }
    public void Next()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Game.loopCount += 1;
    }
}
