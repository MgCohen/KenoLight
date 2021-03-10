﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardNumber : MonoBehaviour
{
    public Image numberSprite;
    public GameObject background;

    public Color markedColor;
    public Color unmarkedColor;



    public int number;

    public void Setup(int i)
    {
        if (i == number)
        {
            return;
        }
        numberSprite.sprite = NumberSprites.main.sprites[i - 1];
        number = i;
        Set(false);
    }

    public void Set(bool state)
    {
        numberSprite.color = (state) ? markedColor : unmarkedColor;
        background.SetActive(state);
    }
}
