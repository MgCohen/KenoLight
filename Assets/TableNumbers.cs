using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableNumbers : MonoBehaviour
{
    public Vector2 start;
    public Vector2 offset;

    public List<GameObject> numbers = new List<GameObject>();
    public Color color;

    public Image counter;
    public void SetNumber(int number)
    {
        numbers[number - 1].SetActive(true);
    }

    public void SetCount(int count)
    {
        counter.sprite = SpriteLoader.Main.sprites[count - 1];
    }

}
