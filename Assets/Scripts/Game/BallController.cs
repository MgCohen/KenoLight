using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

#if UNITY_EDITOR
using System.Linq;
#endif
public class BallController : MonoBehaviour
{
    [Header("Entry")]
    public List<Transform> entryPoints = new List<Transform>();

    public Vector3[] _entryPath;
    public Transform spawner;
    public float moveTime;
    public float moveDelay;
    public float entryScaleSize;

    [Header("Exit")]
    public List<Transform> exitPoints = new List<Transform>();

    public Vector3[] _exitPath;
    public float exitTime;
    public float exitScaleSize;
    public Pipe pipe;


    [Header("Keno Settings")]
    public float kenoMoveTime;
    public List<Transform> kenoEntry = new List<Transform>();
    public Vector3[] _kenoPath;

    [Header("Settings")]
    public float waitTime;
    public List<NumberBall> balls = new List<NumberBall>();
    public CustomAnim ballDrawer;
    public Game game;

    private bool _playing = false;

    int currentNumber;
    bool isKeno;
    NumberBall currentBall;

    public void Draw(int number, bool end = false)
    {
        // if (_entryPath == null) SetPath();
        currentNumber = number;
        isKeno = end;
        //Trigger Draw Animation

        //SELECT ANIMATION
        ballDrawer.Play(1, SpawnBall);
    }

    public void SpawnBall()
    {
        currentBall = GetBall(spawner);

        //moveSound
        if (!isKeno) SoundManager.EntryNumber();
        else SoundManager.EntryKeno();

        //move
        var path = isKeno ? _kenoPath : _entryPath;
        var time = isKeno ? kenoMoveTime : moveTime;

        currentBall.transform.DOScale(entryScaleSize, time * 0.9f).SetDelay(moveDelay);

        var t = currentBall.transform.DOPath(path, time, PathType.CatmullRom).SetDelay(moveDelay).SetEase(Ease.OutQuad).OnStart(() =>
        {
            ballDrawer.Play(2);
            CallNumber();
        });

        //if keno, set reveal
        var o = currentBall;
        if (isKeno) t.OnComplete(() => ShowNumber(currentNumber, o.number));
        else ShowNumber(currentNumber, currentBall.number);

    }

    public void CallNumber()
    {
        SoundManager.PlayNumberVoice(currentNumber);

        var o = currentBall;
        currentBall.transform.DOPath(_exitPath, exitTime, PathType.CatmullRom).SetDelay(waitTime).SetEase(Ease.OutQuad)
            .OnStart(() => { SoundManager.ExitNumber(); DropPipe(o); game.SetTable(); })
            .OnComplete(() => o.transform.SetParent(pipe.ballHolder));
        currentBall.transform.DOScale(exitScaleSize, exitTime * 0.85f).SetDelay(waitTime);
    }

    private void DropPipe(NumberBall o)
    {
        pipe.Drop(this, o);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _entryPath = entryPoints.Select(x => x.position).ToArray();
        _exitPath = exitPoints.Select(x => x.position).ToArray();
        _kenoPath = kenoEntry.Select(x => x.position).ToArray();
    }
#endif
    private static void ShowNumber(int number, Image image)
    {
        image.sprite = SpriteLoader.Main.sprites[number - 1];
    }

    private void Stop()
    {
        _playing = false;
    }

    private void Resume()
    {
        _playing = true;
    }

    public NumberBall GetBall(Transform parent)
    {
        var target = balls[0];
        balls.Remove(target);
        balls.Add(target);
        target.transform.SetParent(parent);
        target.transform.localPosition = Vector3.zero;
        target.gameObject.SetActive(true);
        target.transform.localScale = new Vector3(2.1f, 2.1f, 1);
        return (target);
    }

    public void PoolBall(NumberBall ball)
    {
        ball.gameObject.SetActive(false);
    }
}

