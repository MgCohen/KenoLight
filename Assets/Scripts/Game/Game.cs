using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    [Header("Confi Data")] 
    public string  sorteioId;
    public                        bool offlineMode = true;
    
    [NonSerialized] public Sorteio sorteio;

    public List<int> usedBalls = new List<int>();
    public CardView[] cards = new CardView[4];

    public CustomAnim globo;

    [Header("Components")] 
    [SerializeField] private WinnerPanel winnerPanel = default;

    private void Start()
    {
        NetworkInterface.Instance.RequestSorteio(sorteioId,offlineMode,Setup);
        // winnerPanel.ShowWinners(new List<Card>(), 6);
    }

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
            var index     = Array.IndexOf(sorteio.winnerBalls,number);
            var winnersId = sorteio.winners[index];
            var winners   = sorteio.cards.Where(card => winnersId.Exists(x => x == card.id)).ToArray();
            
            if(index == 2 && usedBalls.Count <= sorteio.acumuladoBallCount)
            {
                index = 3;
            }

            yield return winnerPanel.ShowWinners(winners, index + 4);
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
