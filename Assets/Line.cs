using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Line : MonoBehaviour
{

  public TextMeshProUGUI id;
  public TextMeshProUGUI estabelecimento;
  public List<Image> numbers = new List<Image>();


  Card currentCard;

  public void Setup(Card card)
  {
    if (card == currentCard) return;

    currentCard = card;
    id.text = card.codigo.ToString();
    estabelecimento.text = card.estabelecimento;

    // Debug.Log(1);
    for (int i = 0; i < numbers.Count; i++)
    {
      numbers[i].sprite = NumberSprites.main.sprites[card.numbers[i] - 1];
    }
  }

}
