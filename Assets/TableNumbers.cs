using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableNumbers : MonoBehaviour
{
    public Vector2 start;
    public Vector2 offset;

    public GameObject numberPrefab;
    public Transform tableHolder;
    public Color color;

    public Image counter;

    public void SetNumber(int number)
    {
        var x = (number - 1) % 10;
        var y = (number - 1) / 10;
        var o = Instantiate(numberPrefab, tableHolder);
        var image = o.GetComponent<Image>();
        image.sprite = NumberSprites.main.sprites[number - 1];
        image.color = color;
        var pos = start + new Vector2(offset.x * x, offset.y * y);
        o.transform.localPosition = pos;
    }

    public void SetCount(int count)
    {
        counter.sprite = NumberSprites.main.sprites[count - 1];
    }
}
