using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Line : MonoBehaviour
{

    public TextMeshProUGUI cardId;
    public TextMeshProUGUI estabelecimento;
    public List<TextMeshProUGUI> numbers = new List<TextMeshProUGUI>();
    private int id;
    public void Setup(Card card)
    {
        // Debug.Log(cardId);
        if (card.codigo == id) return;

        id = card.codigo;
        cardId.text = card.codigo.ToString();
        estabelecimento.text = card.estabelecimento;

        for (var i = 0; i < numbers.Count; i++)
        {
            //numbers[i].sprite = SpriteLoader.Main.sprites[card.numbers[i] - 1];
            numbers[i].text = (card.numbers[i] - 1).ToString();
        }
    }

    public void Setup(Card card, Player player)
    {
        id = card.codigo;
        cardId.text = id.ToString();
        estabelecimento.text = card.estabelecimento;

        var missingNumbers = player.missingNumbers;

        for (var i = 0; i < numbers.Count; i++)
        {
            if (i >= missingNumbers.Count) numbers[i].enabled = false;
            else
            {
                numbers[i].enabled = true;
                //numbers[i].sprite = SpriteLoader.Main.sprites[missingNumbers[i] - 1];
                numbers[i].text = (missingNumbers[i]).ToString();
            }
        }
    }

}
