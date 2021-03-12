using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableNumbers : MonoBehaviour
{
  public Vector2 start;
  public Vector2 offset;

  public Image numberPrefab;
  public Transform tableHolder;
  public Color color;

  public Image counter;

  public void SetNumber(int number)
  {
    var x = (number - 1) % 10;
    var y = (number - 1) / 10;
    var numberImage = Instantiate(numberPrefab, tableHolder);
    numberImage.sprite = SpriteLoader.Main.sprites[number - 1];
    numberImage.color = color;
    var pos = start + new Vector2(offset.x * x, offset.y * y);
    numberImage.transform.localPosition = pos;
  }

  public void SetCount(int count)
  {
    counter.sprite = SpriteLoader.Main.sprites[count - 1];
  }
}
