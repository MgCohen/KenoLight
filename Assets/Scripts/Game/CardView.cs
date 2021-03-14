using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
  private int _id;
  public TextMeshProUGUI cardId;
  public TextMeshProUGUI cardSource;

  public List<CardNumber> numbers = new List<CardNumber>();

  public void Setup(Card card, List<int> calledNumbers)
  {
    // if (card.codigo == _id) return false;
    var set = calledNumbers != null;
    
    _id = card.codigo;
    cardId.text = _id.ToString();
    cardSource.text = card.estabelecimento;

    for (var i = 0; i < numbers.Count; i++)
    {
      var num = numbers[i];
      num.Setup(card.numbers[i]);
      if (set) num.Set(calledNumbers.Contains(num.number));
    }
    // return true;
  }
  
  public void Setup(Card card, int[] calledBalls, int maxIndex)
  {
    
    _id             = card.codigo;
    cardId.text     = _id.ToString();
    cardSource.text = card.estabelecimento;

    for (var i = 0; i < numbers.Count; i++)
    {
      var num = numbers[i];
      num.Setup(card.numbers[i]);
      num.Set(calledBalls.IndexOf(num.number,maxIndex) >= 0);
    }
    // return true;
  }
}
