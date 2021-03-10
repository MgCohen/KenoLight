using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    [Header("Confi Data")]
    public string sorteioId;
    public bool offlineMode = true;

    [NonSerialized] public Sorteio sorteio;

    public List<int> usedBalls = new List<int>();
    public CardView[] cards = new CardView[4];

    public CustomAnim globo;
    public BallController control;

    [Header("Timer")]
    public float drawTime;

    [Header("Components")]
    [SerializeField] private WinnerPanel winnerPanel = default;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) Request();
    }

    public void Request()
    {
        NetworkInterface.Instance.RequestSorteio(sorteioId, offlineMode, Setup);

    }

    public void Setup(Sorteio novoSorteio)
    {
        sorteio = novoSorteio;
        Debug.Log(sorteio.cards.Count);
        Debug.Log(sorteio.cards[0].numbers[2]);
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].Setup(sorteio.cards[i]);
        }
        StartDraw();
    }

    public void Jeitinho()
    {
    }

    public void StartDraw()
    {
        StartCoroutine(Drawing());
    }

    IEnumerator Drawing()
    {
        globo.Resume();
        List<int> missingBalls = sorteio.balls;
        yield return new WaitForSeconds(drawTime);
        //pega bola
        var number = missingBalls[0];
        missingBalls.Remove(number);

        //wait for ball draw
        yield return control.Draw(number);

        //Arruma e marca
        usedBalls.Add(number);
        Sort(usedBalls.Count - 1);

        //Verifica ganhador
        if (sorteio.winnerBalls.Contains(number))
        {
            var index = Array.IndexOf(sorteio.winnerBalls, number);
            var winnersId = sorteio.winners[index];
            var winners = sorteio.cards.Where(card => winnersId.Exists(x => x == card.id)).ToArray();

            if (index == 2 && usedBalls.Count <= sorteio.acumuladoBallCount)
            {
                index = 3;
            }

            yield return winnerPanel.ShowWinners(winners, index + 4);
        }

        //espera
    }


    public void Sort(int round)
    {
        Debug.Log(1);
        Debug.Log(sorteio.topPlayers.Count);
        var topPlayers = sorteio.topPlayers[round];
        Debug.Log(2);
        for (int i = 0; i < cards.Length; i++)
        {
            Debug.Log(3);
            var card = sorteio.cards.Find(x => x.id == topPlayers[i].id);
            Debug.Log(4);
            if (cards[i].Setup(card)) cards[i].CatchUp(usedBalls);
            else cards[i].Mark(usedBalls[round - 1]);
            Debug.Log(5);
        }
    }

}
