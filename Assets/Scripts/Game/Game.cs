using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Game : MonoBehaviour
{
  [Header("Config Data")]
  public string sorteioId;
  public bool offlineMode = true;

  private Sorteio _sorteio;

  public List<int> usedBalls = new List<int>();

  [Header("Views")]
  public CardView[] cards = new CardView[4];
  public Line[] lines = new Line[15];

  public CustomAnim globo;
  public BallController control;

  [Header("Timer")]
  public float drawTime;


  [Header("Visuals")]
  public ValueSetter values;
  public TableNumbers table;


  private void Start()
  {
    GC.Collect();
    GC.WaitForPendingFinalizers();
    Invoke(nameof(Request), 3);
  }

  public void Request()
  {
    NetworkInterface.Instance.RequestSorteio(sorteioId, offlineMode, Setup);
  }

  private void Setup(Sorteio novoSorteio)
  {
    _sorteio = novoSorteio;
    values.Set(_sorteio);
    SetCards();
    SetLines();
    StartDraw();
  }

  private void SetCards()
  {
    for (var i = 0; i < cards.Length; i++)
    {
      cards[i].Setup(_sorteio.cards[i]);
    }
  }

  private void SetLines()
  {
    for (var i = 0; i < lines.Length; i++)
    {
      if (i < _sorteio.cards.Count)
        lines[i].Setup(_sorteio.cards[i]);
      else
        lines[i].gameObject.SetActive(false);
    }
  }

  private void StartDraw()
  {
    StartCoroutine(Drawing());
  }

  private IEnumerator Drawing()
  {
    globo.Resume();
    var missingBalls = _sorteio.balls;

    while (missingBalls.Count > 0)
    {

      yield return new WaitForSeconds(drawTime);
      //pega bola
      var number = missingBalls[0];
      missingBalls.Remove(number);

      //wait for ball draw
      yield return control.Draw(number);

      table.SetNumber(number);
      //Arruma e marca
      usedBalls.Add(number);
      table.SetCount(usedBalls.Count);
      Sort(usedBalls.Count - 1);

      //Verifica ganhador
      if (!_sorteio.winnerBalls.Contains(number)) continue;
      
      var index = Array.IndexOf(_sorteio.winnerBalls, number);
      Debug.Log($"{index} cout: {_sorteio.winners.Count}");

      // var winnersId = _sorteio.winners[index];
      // var winners   = _sorteio.cards.Where(card => winnersId.Exists(x => x == card.codigo)).ToArray();

      Win(index);
      //if (index == 2 && usedBalls.Count <= sorteio.acumuladoBallCount)
      //{
      //    index = 3;
      //}

      //yield return winnerPanel.ShowWinners(winners, index + 4);
    }

    DOTween.KillAll();
    DOTween.ClearCachedTweens();
    SceneManager.LoadScene(1);

    //espera
  }


  private void Sort(int round)
  {
    var topPlayers = _sorteio.topPlayers[round];
    var topCards = topPlayers.Select(x => x.id).Distinct().ToArray();
    for (var i = 0; i < lines.Length; i++)
    {
      if (!lines[i].gameObject.activeInHierarchy) return;
      // Debug.Log($"{i} top players count: {topPlayers.Count}");

      var card = _sorteio.cards.Find(x => x.codigo == topPlayers[i].id);
      var topCard = _sorteio.cards.Find(x => x.codigo == topCards[i]);
      if (i < cards.Length)
      {
        if (cards[i].Setup(topCard))
          cards[i].CatchUp(usedBalls);
        else
          cards[i].Mark(usedBalls[round]);
      }

      i = Mathf.Clamp(i, 0, 14);
      lines[i].Setup(card, topPlayers[i]);
    }
  }

  private void Win(int prizeIndex)
  {
    values.SetPrize(prizeIndex);
    //get winner
    //show screen
  }

}
