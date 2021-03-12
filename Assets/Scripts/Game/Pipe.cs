using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pipe : MonoBehaviour
{
    public Transform[] balls = new Transform[4];
    public Transform[] targets = new Transform[5];

    public float moveTime;
    public Transform ballHolder;

    public void Drop(BallController controller, NumberBall Ball)
    {
        for (int i = balls.Length - 1; i >= 0; i--)
        {
            if (!balls[i]) continue;

            var t = balls[i].transform.DOMove(targets[i + 1].position, moveTime);
            var o = balls[i].gameObject;
            if (i == 3) t.OnComplete(() => o.SetActive(false)) ;
            else balls[i + 1] = balls[i];

            balls[i] = null;
        }

        balls[0] = Ball.transform;
    }
}
