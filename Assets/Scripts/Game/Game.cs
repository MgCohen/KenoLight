﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{

    Sorteio sorteio;

    public List<int> usedBalls = new List<int>();
    public CardView[] cards = new CardView[4];

    public CustomAnim globo;
    public void Setup(Sorteio novoSorteio)
    {
        sorteio = novoSorteio;
        StartDraw();
    }

    public void StartDraw()
    {

    }

    IEnumerator Drawing()
    {
        List<int> missingBalls = sorteio.balls;
        yield return null;
        //pega bola
        var number = missingBalls[0];
        missingBalls.Remove(number);
        //puxa bola
        //espera
        //reseta bola
        //sort
        usedBalls.Add(number);
        Sort(usedBalls.Count - 1);
        if (sorteio.winnerBalls.Contains(number))
        {
            var index = sorteio.winnerBalls.IndexOf(number);
            var winners = sorteio.winners[index];
            if(index == 2 && usedBalls.Count <= sorteio.acumuladoBallCount)
            {
                index = 3;
            }

            //GET WINNERS ( INDEX, WINNERS)
        }
        //espera
    }

    public void Sort(int round)
    {
        var topPlayers = sorteio.topPlayers[round];
        for (int i = 0; i < cards.Length; i++)
        {
            var card = sorteio.cards.Find(x => x.id == topPlayers[i].id);
            if (cards[i].Setup(card)) cards[i].CatchUp(usedBalls);
            else cards[i].Mark(usedBalls[usedBalls.Count - 1]);
        }
    }

}
