using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TestScript : MonoBehaviour
{
  public TextMeshProUGUI counter;
  public float tempo = 300f;
  private WaitForSeconds waitTime = new WaitForSeconds(1);
  private void Start()
  {
    Invoke(nameof(Next), tempo);
    StartCoroutine(Count());
  }
  private IEnumerator Count()
  {
    while (tempo > 0)
    {
      counter.text = (tempo--).ToString() + "s";
      yield return waitTime;
    }
  }
  public void Next()
  {
    Destroy(DOTween.instance.gameObject);
    StopAllCoroutines();
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    Game.loopCount += 1;
  }
}
