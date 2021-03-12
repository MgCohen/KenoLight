
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class TestScript : MonoBehaviour
{
  public TextMeshProUGUI counter;
  public float tempo = 300f;
  private readonly Vector3 rotate = new Vector3(0, 0, -0.1f);
  private void Start()
  {
    GC.Collect();
    GC.WaitForPendingFinalizers();
    counter.text += " " + Game.loopCount;
    Invoke(nameof(Next), tempo);
  }
  private void Update()
  {
    counter.transform.Rotate(rotate);
  }
  public void Next()
  {
    Destroy(DOTween.instance.gameObject);
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    Game.loopCount += 1;
  }
}
