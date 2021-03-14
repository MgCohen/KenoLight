
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class TestScript : MonoBehaviour
{
  public           TextMeshProUGUI counter;
  public           float           tempo        = 300f;
  private readonly Vector3         _rotate      = new Vector3(0, 0, -90f);
  private          float           _timeCounter = 0;
  private readonly Api             _api         = new Api("https://postman-echo.com/get");
  private void Start()
  {
    GC.Collect();
    GC.WaitForPendingFinalizers();
    counter.text += "\n" + Game.LoopCount;
   
    // _api.Get("").OnComplete((data) =>
    // {
    //   Debug.Log(System.Text.Encoding.UTF8.GetString(data.ToArray()));
    // });
  }
  
  private void Update()
  {
    counter.transform.Rotate(_rotate * Time.deltaTime);

    if (_timeCounter >= tempo)
    {
      _timeCounter = 0;
      Next();
      return;
    }

    _timeCounter += Time.deltaTime;
  }


  private static void Next()
  {
    DOTween.Clear(true);
    Game.LoopCount += 1;
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
  }
}
