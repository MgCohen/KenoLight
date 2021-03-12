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

    private Vector3[] _entryPath;
    public  Transform spawner;
    public  float     moveTime;
    public  float     moveDelay;
    public  float     entryScaleSize;

    [Header("Exit")]
    public List<Transform> exitPoints = new List<Transform>();

    private Vector3[] _exitPath;
    public  float     exitTime;
    public  float     exitScaleSize;
    public  Pipe      pipe;


    [Header("Keno Settings")]
    public float kenoMoveTime;
    public  List<Transform> kenoEntry = new List<Transform>();
    private Vector3[]       _kenoPath;

    [Header("Settings")]
    public float waitTime;
    public GameObject ballPrefab;
    public CustomAnim ballDrawer;

    private bool _playing = false;

    public IEnumerator Draw(int number, bool end = false)
    {
        if (_entryPath == null) SetPath();

        //Trigger Draw Animation
        Stop();
        //SELECT ANIMATION
        ballDrawer.Play("Vermelho", Resume);

        //wait for animation
        while (!_playing) yield return null;

        //spawn
        //SELECT PREFAB
        var o = Instantiate(ballPrefab, spawner);

        //moveSound
        if (!end) SoundManager.EntryNumber();
        else SoundManager.EntryKeno();

        //move
        var path = end ? _kenoPath : _entryPath;
        var time = end ? kenoMoveTime : moveTime;
        o.transform.DOScale(entryScaleSize, time * 0.9f).SetDelay(moveDelay);
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
        SoundManager.PlayNumberVoice(number);


        //wait for screen time
        yield return new WaitForSeconds(waitTime);

        //exit sound
        SoundManager.ExitNumber();

        //exit
        DropPipe(o);
        o.transform.DOPath(_exitPath, exitTime, PathType.CatmullRom).SetEase(Ease.OutQuad).OnComplete(() => o.transform.SetParent(pipe.ballHolder));
        o.transform.DOScale(exitScaleSize, exitTime * 0.85f);

        //return control
    }

    private void DropPipe(GameObject o)
    {
        pipe.Drop(o.transform);
    }

    private void SetPath()
    {
        _entryPath = entryPoints.Select(x => x.position).ToArray();
        _exitPath = exitPoints.Select(x => x.position).ToArray();
        _kenoPath = kenoEntry.Select(x => x.position).ToArray();
    }

    private static void ShowNumber(int number, Image image)
    {
        image.sprite = NumberSprites.Main.sprites[number - 1];
    }

    private void Stop()
    {
        _playing = false;
    }

    private void Resume()
    {
        _playing = true;
    }
}

