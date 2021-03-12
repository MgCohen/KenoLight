using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Game : MonoBehaviour
{
  [Header("Config Data")]
  public string sorteioId;
  public bool offlineMode = true;

  //////

  // private Sorteio _sorteio;

  public List<int> usedBalls = new List<int>();

  [Header("Views")]
  public CardView[] cards = new CardView[4];
  public Line[] lines = new Line[15];

  public CustomAnim globo;
  public BallController control;

  [Header("Timer")]
  public float drawTime;

  [Header("Components")]
  // [SerializeField] private WinnerPanel winnerPanel = default;

  [Header("Visuals")]
  public ValueSetter values;
  public TableNumbers table;

  public static short loopCount = 1;
  public static string sorteioStart;
  public TextMeshProUGUI contador;

  private void Start()
  {
    if (string.IsNullOrEmpty(sorteioStart)) sorteioStart = DateTime.Now.ToString("HH:mm");
    GC.Collect();
    GC.WaitForPendingFinalizers();
    Screen.SetResolution(1280, 720, true, 30);
    Invoke("Request", 3);
    contador.text = sorteioStart + "  " + loopCount.ToString() + "  " + DateTime.Now.ToString("HH:mm");
  }

  public void Request()
  {
    NetworkInterface.Instance.RequestSorteio(sorteioId, offlineMode, Setup);
  }

  private void Setup(Sorteio _sorteio)
  {
    values.Set(_sorteio);
    SetCards(_sorteio);
    SetLines(_sorteio);
    StartDraw(_sorteio);
  }

  private void SetCards(Sorteio _sorteio)
  {
    var carts = _sorteio.cards.Values.Take(cards.Length).ToList();
    for (var i = 0; i < cards.Length; i++)
    {
      cards[i].Setup(carts[i], null);
    }
  }

  private void SetLines(Sorteio _sorteio)
  {
    var carts = _sorteio.cards.Values.Take(lines.Length).ToList();

    for (var i = 0; i < lines.Length; i++)
    {
      if (i < _sorteio.cards.Count) lines[i].Setup(carts[i]);
      else lines[i].gameObject.SetActive(false);
    }
  }

  private void StartDraw(Sorteio _sorteio)
  {
    StartCoroutine(Drawing(_sorteio));
  }

  private IEnumerator Drawing(Sorteio _sorteio)
  {
    globo.Resume();
    // var missingBalls = _sorteio.balls;

    for (int i = 0; i < _sorteio.balls.Count; i++)
    {

      yield return new WaitForSeconds(drawTime);
      //pega bola
      var number = _sorteio.balls[i];
      //wait for ball draw
      yield return control.Draw(number);

      table.SetNumber(number);
      //Arruma e marca
      usedBalls.Add(number);
      table.SetCount(usedBalls.Count);
      Sort(usedBalls.Count - 1, _sorteio);

      //Verifica ganhador
      if (!_sorteio.winnerBalls.Contains(number)) continue;

      var index = Array.IndexOf(_sorteio.winnerBalls, number);
      // Debug.Log($"{index} cout: {_sorteio.winners.Count}");

      // var winnersId = _sorteio.winners[index];
      // var winners   = _sorteio.cards.Where(card => winnersId.Exists(x => x == card.codigo)).ToArray();

      Win(index);
      //if (index == 2 && usedBalls.Count <= sorteio.acumuladoBallCount)
      //{
      //    index = 3;
      //}

      //yield return winnerPanel.ShowWinners(winners, index + 4);
    }

    GC.Collect();
    GC.WaitForPendingFinalizers();
    DOTween.KillAll();
    DOTween.ClearCachedTweens();
    SceneManager.LoadScene(1);
    _sorteio = null;
    //espera
  }


  private void Sort(int round, Sorteio _sorteio)
  {
    var topPlayers = _sorteio.topPlayers[round];
    var topCards = topPlayers.Select(x => x.id).Distinct().ToArray();

    for (var i = 0; i < lines.Length; i++)
    {
      if (!lines[i].gameObject.activeInHierarchy) return;
      // Debug.Log($"{i} top players count: {topPlayers.Count}");

      var card = _sorteio.cards[topPlayers[i].id];
      var topCard = _sorteio.cards[topCards[i]];

      if (i < cards.Length)
      {
        // if (cards[i].Setup(topCard))
        cards[i].Setup(topCard, usedBalls);
        // cards[i].CatchUp(usedBalls);
        // else
        // cards[i].Mark(usedBalls[round]);
      }

      // i = Mathf.Clamp(i, 0, 14);
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
