using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSprites : MonoBehaviour
{

    public static NumberSprites Main;
    private void Awake()
    {
        Main = this;
    }

    public Sprite[] sprites = new Sprite[90];
}
