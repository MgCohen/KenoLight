using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{

    public static SpriteLoader main;
    private void Awake()
    {
        if (main) Destroy(main.gameObject);
        main = this;
    }

    public Sprite[] sprites = new Sprite[90];
}
