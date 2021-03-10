using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
  [Header("Entry")]
  public List<Transform> entryPoints = new List<Transform>();
  Vector3[] entryPath;
  public Transform spawner;
  public float moveTime;
  public float moveDelay;
  public float entryScaleSize;

  [Header("Exit")]
  public List<Transform> exitPoints = new List<Transform>();
  Vector3[] exitPath;
  public float exitTime;
  public float exitScaleSize;

  [Header("Keno Settings")]
  public float kenoMoveTime;
  public List<Transform> kenoEntry = new List<Transform>();
  Vector3[] kenoPath;

  [Header("Tube")]
  public Transform[] tubePath;
  public List<Transform> ball;

  [Header("Settings")]
  public float waitTime;
  public GameObject ballPrefab;
  public CustomAnim ballDrawer;

  bool playing = false;

  public IEnumerator Draw(int number, bool end = false)
  {
    if (entryPath == null) SetPath();

    //Trigger Draw Animation
    Stop();
    //SELECT ANIMATION
    ballDrawer.Play("Vermelho", Resume);

    //wait for animation
    while (!playing) yield return null;

    //spawn
    //SELECT PREFAB
    var o = Instantiate(ballPrefab, spawner);

    //moveSound

    //move
    var path = end ? kenoPath : entryPath;
    var time = end ? kenoMoveTime : moveTime;
    o.transform.DOScale(entryScaleSize, time * 0.7f);
    var t = o.transform.DOPath(path, time, PathType.CatmullRom).SetDelay(moveDelay).SetEase(Ease.OutQuad).OnStart(() =>
    {
      ballDrawer.Play("ZoomOut");
    });

    //if keno, set reveal
    if (end) t.OnComplete(() => ShowNumber(number, o.GetComponentsInChildren<Image>()[1]));
    else ShowNumber(number, o.GetComponentsInChildren<Image>()[1]);

    //wait for tween
    yield return t.WaitForCompletion();

    //call

    //wait for screen time
    yield return new WaitForSeconds(waitTime);

    //exit sound


    //exit
    o.transform.DOPath(exitPath, exitTime, PathType.CatmullRom).SetEase(Ease.OutQuad).OnComplete(() => DropPipe(o));
    o.transform.DOScale(exitScaleSize, exitTime * 0.85f);

    //return control
  }

  public void DropPipe(GameObject o)
  {
        Destroy(o, 0.2f);
  }

  public void SetPath()
  {
    entryPath = entryPoints.Select(x => x.position).ToArray();
    exitPath = exitPoints.Select(x => x.position).ToArray();
    kenoPath = kenoEntry.Select(x => x.position).ToArray();
  }

  public void ShowNumber(int number, Image image)
  {
    image.sprite = NumberSprites.main.sprites[number - 1];
  }

  public void Stop()
  {
    playing = false;
  }

  public void Resume()
  {
    playing = true;
  }
}

