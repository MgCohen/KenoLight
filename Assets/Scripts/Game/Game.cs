using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    [Header("Config Data")]
    public string sorteioId;
    public bool offlineMode = true;

    [NonSerialized] public Sorteio sorteio;

    public List<int> usedBalls = new List<int>();

    [Header("Views")]
    public CardView[] cards = new CardView[4];
    public Line[] lines = new Line[15];

    public CustomAnim globo;
    public BallController control;

    [Header("Timer")]
    public float drawTime;

    [Header("Components")]
    [SerializeField] private WinnerPanel winnerPanel = default;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) Request();
    }

    public void Request()
    {
        NetworkInterface.Instance.RequestSorteio(sorteioId, offlineMode, Setup);

    }

    public void Setup(Sorteio novoSorteio)
    {
        sorteio = novoSorteio;

        SetCards();
        SetLines();

        StartDraw();
    }

    public void SetCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].Setup(sorteio.cards[i]);
        }
    }

    public void SetLines()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (i < sorteio.cards.Count) lines[i].Setup(sorteio.cards[i]);
            else lines[i].gameObject.SetActive(false);
        }
    }

    public void StartDraw()
    {
        StartCoroutine(Drawing());
    }

    IEnumerator Drawing()
    {
        globo.Resume();
        List<int> missingBalls = sorteio.balls;

        while (missingBalls.Count > 0)
        {

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
                var winners = sorteio.cards.Where(card => winnersId.Exists(x => x == card.codigo)).ToArray();

                if (index == 2 && usedBalls.Count <= sorteio.acumuladoBallCount)
                {
                    index = 3;
                }

                yield return winnerPanel.ShowWinners(winners, index + 4);
            }
        }

        //espera
    }


    public void Sort(int round)
    {
        var topPlayers = sorteio.topPlayers[round];
        for (int i = 0; i < lines.Length; i++)
        {
            if (!lines[i].gameObject.activeInHierarchy) return;

            var card = sorteio.cards.Find(x => x.codigo == topPlayers[i].id);
            if (i < cards.Length)
            {
                if (cards[i].Setup(card))
                    cards[i].CatchUp(usedBalls);
                else
                    cards[i].Mark(usedBalls[round]);
            }

            lines[i].Setup(card);
        }
    }

}
