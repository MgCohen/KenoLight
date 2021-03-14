using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Game : MonoBehaviour
{
  [Header("Config Data")]
  public string sorteioId;
  public bool offlineMode = true;

  //////

  // [SerializeField] private Sorteio sorteio;
  private int       _prizeCount;
  // public  List<int> usedBalls = new List<int>();
  private  int _usedBallsCount = 0;

  [Header("Views")]
  public CardView[] cards = new CardView[4];
  public Line[] lines = new Line[15];

  public CustomAnim globo;
  public BallController control;

  [Header("Timer")]
  public float drawTime;
  private WaitForSeconds _drawTimer;
  
  [Header("Components")]
  // [SerializeField] private WinnerPanel winnerPanel = default;

  [Header("Visuals")]
  public ValueSetter values;
  public TableNumbers table;

  public static  short           LoopCount = 1;
  private static string          _sorteioStart;
  public         TextMeshProUGUI contador;
  
  private void Start()
  {
    _drawTimer = new WaitForSeconds(drawTime);
    
    if (string.IsNullOrEmpty(_sorteioStart)) _sorteioStart = DateTime.Now.ToString("HH:mm");
    GC.Collect();
    GC.WaitForPendingFinalizers();
    // Screen.SetResolution(1920, 1080, true, 30);
    contador.text = _sorteioStart + "  " + LoopCount.ToString() + "  " + DateTime.Now.ToString("HH:mm");
    // Invoke("Request", 3);
    Request();
  }


  private void Request()
  {
    NetworkInterface.Instance.RequestSorteio(sorteioId, offlineMode, Setup);
  }

  private void Setup(Sorteio sorteio)
  {
    Debug.Log("start Sorteio");
    values.Set(sorteio);
    SetCards(sorteio);
    SetLines(sorteio);
    StartDraw(sorteio);
  }

  private void SetCards(Sorteio sorteio)
  {
    var carts = sorteio.cards.Values;
    var i     = 0;
    foreach (var cart in carts)
    {
      cards[i].Setup(cart);
      ++i;
      if (i >= cards.Length) break;
    }
  }

  private void SetLines(Sorteio sorteio)
  {

    var carts = sorteio.cards.Values;
    var i     = 0;
    foreach (var cart in carts)
    {
      lines[i].Setup(cart);
      ++i;
      if (i >= lines.Length) break;
    }
  }

  private void StartDraw(Sorteio sorteio)
  {
    StartCoroutine(Drawing(sorteio));
  }

  private IEnumerator Drawing(Sorteio sorteio)
  {
    globo.Resume();
    // var missingBalls = sorteio.balls;

    for (var i = 0; i < sorteio.balls.Length; i++)
    {

      yield return _drawTimer;
      //pega bola
      var number = sorteio.balls[i];
      //wait for ball draw
      yield return control.Draw(number);
      
      table.SetNumber(number);
      //Arruma e marca
      // usedBalls.Add(number);
      ++_usedBallsCount;
      
      table.SetCount(_usedBallsCount);
      Sort(_usedBallsCount - 1, sorteio);

      //Verifica ganhador
      if (sorteio.winnerBalls[_prizeCount] != number) continue;

      ++_prizeCount;
      Win(_prizeCount);
    }

    GC.Collect();
    GC.WaitForPendingFinalizers();
    DOTween.KillAll();
    DOTween.ClearCachedTweens();
    SceneManager.LoadScene(1);
    sorteio = null;
    //espera
  }


  private void Sort(int round, Sorteio sorteio)
  {
    if(round > 0) sorteio.topPlayers[round - 1] = null;
    
    var topPlayers = sorteio.topPlayers[round];
    var lastId     = 0;
    var cardIndex  = 0;
    
    for (var i = 0; i < lines.Length; i++)
    {
      if (!lines[i].gameObject.activeInHierarchy) return;

      var id      = topPlayers[i].id;
      var card = sorteio.cards[id];
      
      //Só aplicar nas 4 primeiras cartelas, caso não sejam repetidas
      if (cardIndex < cards.Length && id != lastId)
      {
        cards[cardIndex].Setup(card,sorteio.balls, _usedBallsCount - 1);
        lastId = id;
        ++cardIndex;
      }

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
