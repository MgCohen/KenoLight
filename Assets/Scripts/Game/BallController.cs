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

    private bool _playing = false;

    public IEnumerator Draw(int number, bool end = false)
    {
        // if (_entryPath == null) SetPath();

        //Trigger Draw Animation
        Stop();
        //SELECT ANIMATION
        ballDrawer.Play(1, Resume);

        //wait for animation
        while (!_playing) yield return null;

        //spawn
        //SELECT PREFAB
        var o = GetBall(spawner);

        //moveSound
        if (!end) SoundManager.EntryNumber();
        else SoundManager.EntryKeno();

        //move
        var path = end ? _kenoPath : _entryPath;
        var time = end ? kenoMoveTime : moveTime;

        o.transform.DOScale(entryScaleSize, time * 0.9f).SetDelay(moveDelay);

        var t = o.transform.DOPath(path, time, PathType.CatmullRom).SetDelay(moveDelay).SetEase(Ease.OutQuad).OnStart(() =>
        {
            ballDrawer.Play(2);
        });

        //if keno, set reveal
        if (end) t.OnComplete(() => ShowNumber(number, o.number));
        else ShowNumber(number, o.number);

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
        target.transform.localScale = new Vector3(2.1f, 2.1f,1);
        return (target);
    }

    public void PoolBall(NumberBall ball)
    {
        ball.gameObject.SetActive(false);
    }
}

