
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
    numberSprite.sprite = SpriteLoader.Main.sprites[i - 1];
    number = i;
  }

  public void Set(bool state)
  {
    numberSprite.color = (state) ? markedColor : unmarkedColor;
    background.SetActive(state);
  }
}
