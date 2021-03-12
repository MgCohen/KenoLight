using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class CardView : MonoBehaviour
{
    private int             _id;
    public  TextMeshProUGUI cardId;
    public  TextMeshProUGUI cardSource;

    public List<CardNumber> numbers = new List<CardNumber>();

    public bool Setup(Card card)
    {
        if (card.codigo == _id) return false;
        _id              = card.codigo;
        cardId.text     = _id.ToString();
        cardSource.text = card.estabelecimento;

        for (var i = 0; i < numbers.Count; i++)
        {
            numbers[i].Setup(card.numbers[i]);
        }
        return true;
    }

    public void Mark(int i)
    {
        var num = numbers.Find(x => x.number == i);
        if (num) num.Set(true);
    }

    public void CatchUp(List<int> calledNumbers)
    {
        foreach (var n in calledNumbers)
            Mark(n);
    }
}
