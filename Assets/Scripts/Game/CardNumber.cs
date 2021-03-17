using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CardNumber : MonoBehaviour
{
  public TextMeshProUGUI numberSprite;
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
        //numberSprite.sprite = SpriteLoader.Main.sprites[i - 1];
        numberSprite.text = i.ToString();
    number = i;
    // Set(false);
  }

  public void Set(bool state)
  {
    numberSprite.color = (state) ? markedColor : unmarkedColor;
    background.SetActive(state);
  }
}
