
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class TestScript : MonoBehaviour
{
  public           TextMeshProUGUI counter;
  public           float           tempo   = 300f;
  private readonly Vector3         _rotate = new Vector3(0, 0, -90f);
  private          float           timeCounter    = 0;
  private void Start()
  {
    GC.Collect();
    GC.WaitForPendingFinalizers();
    counter.text += "\n" + Game.LoopCount;
    Invoke(nameof(Next), tempo);
    // InvokeRepeating(nameof(Load1s), 60, 60);
  }
  private void Update()
  {
    counter.transform.Rotate(_rotate * Time.deltaTime);

    if (timeCounter >= tempo)
    {
      timeCounter = 0;
      Next();
      return;
    }

    timeCounter += Time.deltaTime;
  }


  public void Next()
  {
    Destroy(DOTween.instance.gameObject);
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    Game.LoopCount += 1;
  }
}
