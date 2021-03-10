using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSprites : MonoBehaviour
{

    public static NumberSprites main;
    private void Awake()
    {
        main = this;
    }

    public Sprite[] sprites = new Sprite[90];
}
