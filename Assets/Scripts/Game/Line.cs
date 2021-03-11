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


        for (int i = 0; i < numbers.Count; i++)
        {
            numbers[i].sprite = NumberSprites.main.sprites[card.numbers[i] - 1];
        }
    }

    public void Setup(Card card, Player player)
    {

        currentCard = card;
        id.text = card.codigo.ToString();
        estabelecimento.text = card.estabelecimento;

        var missingNumbers = player.missingNumbers;

        for (int i = 0; i < numbers.Count; i++)
        {
            if (i >= missingNumbers.Count) numbers[i].enabled = false;
            else
            {
                numbers[i].enabled = true;
                numbers[i].sprite = NumberSprites.main.sprites[missingNumbers[i] - 1];
            }
        }
    }

}
