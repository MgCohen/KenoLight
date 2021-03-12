using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
  [Header("Settings")] public EventHandler handler;
  [Header("Header")] public TextMeshProUGUI tempoTxt;
  public TextMeshProUGUI sorteioTxt;
  public TextMeshProUGUI donateTxt;
  public TextMeshProUGUI acumuladoTxt;

  [Header("Sorteio Especial")]
  [SerializeField] private TextMeshProUGUI _horaEspecial;
  [SerializeField] private TextMeshProUGUI _dataEspecial;
  [SerializeField] private TextMeshProUGUI _valorKenoEspecial;
  [SerializeField] private TextMeshProUGUI _valorKinaEspecial;
  [SerializeField] private TextMeshProUGUI _valorKuadraEspecial;
  [Header("Sorteio Super Especial")]
  [SerializeField] private TextMeshProUGUI _horaSuperEspecial;
  [SerializeField] private TextMeshProUGUI _dataSuperEspecial;
  [SerializeField] private TextMeshProUGUI _valorKenoSuperEspecial;
  [SerializeField] private TextMeshProUGUI _valorKinaSuperEspecial;
  [SerializeField] private TextMeshProUGUI _valorKuadraSuperEspecial;

  [Header("Sorteio Normal")]
  [SerializeField] private TextMeshProUGUI _hora;
  [SerializeField] private TextMeshProUGUI _data;
  [SerializeField] private TextMeshProUGUI _valorKeno;
  [SerializeField] private TextMeshProUGUI _valorKina;
  [SerializeField] private TextMeshProUGUI _valorKuadra;

  [Header("Historico Próximos")]
  [SerializeField] private List<TextMeshProUGUI> linhasAnterior;
  [SerializeField] private List<TextMeshProUGUI> linhasProximo;


  [Header("Tela Oferecimento")]
  [SerializeField] private Image imgOferecimento;

  [Header("Tela Ong")]
  [SerializeField] private Image imgOng;

  [Header("Tela Participantes Especial")]
  [SerializeField] private List<Image> imgPartList;

  [Header("Tela Participantes Super Especial")]
  [SerializeField] private List<Image> imgSuperPartList;

  public List<Sorteio> sorteios;

  [Header("Values")]
  public SorteioValuesUI ativo;
  // public SorteioValuesUI proximo;
  public SorteioValuesUI especial;

  [Header("Tabela")] public TextMeshProUGUI headerTabelaTxt;
  //public LobbyPrizeLine linePrefab;
  public Transform tabelaHolder;
  //public List<LobbyPrizeLine> tabelaLines;




  private bool _hasSorteioAtivo = false;
  private DateTime _sorteioDate;
  //private List<HistoryData> _updateHistory;

  [Header("Chamadas")]
  private bool tocar2m, tocar1m, tocar30s, tocarEncerr = false;
  public AudioData dataAudio;

  [SerializeField] private bool esp;
  [SerializeField] private bool espSE;





  private IEnumerator UpdateTimer()
  {

    while (_hasSorteioAtivo)
    {
      var tempoRestante = _sorteioDate.Subtract(DateTime.Now);
      // Debug.LogWarning(FormatTime(tempoRestante));
      tempoTxt.text = "Próximo Sorteio: " + FormatTime(tempoRestante);



      // Debug.Log("tempo: " + tempoRestante.TotalMinutes);
      if (tempoRestante.TotalMinutes <= 2.0 && !tocar2m && tempoRestante.TotalMinutes >= 1.99)
      {
        Debug.Log("Toquei 2 min");
        SoundSystem.Play(dataAudio.calls[0], AudioType.Voice, 0.25f);
        tocar2m = true;
      }
      else if (tempoRestante.TotalMinutes <= 1.0 && !tocar1m && tempoRestante.TotalMinutes >= 0.9)
      {
        Debug.Log("Toquei tocar1m");
        SoundSystem.Play(dataAudio.calls[1], AudioType.Voice, 0.25f);
        tocar1m = true;
      }
      else if (tempoRestante.TotalMinutes <= 0.5 && !tocar30s && tempoRestante.TotalMinutes >= 0.49)
      {
        Debug.Log("Toquei tocar30s");
        SoundSystem.Play(dataAudio.calls[2], AudioType.Voice, 0.25f);
        tocar30s = true;
      }
      else if (tempoRestante.TotalMinutes <= 0.1 && !tocarEncerr && tempoRestante.TotalMinutes >= 0.04)
      {
        Debug.Log("Toquei encerrou ");
        SoundSystem.Play(dataAudio.calls[3], AudioType.Voice, 0.25f);
        tocarEncerr = true;
      }


      if (tempoRestante.TotalMinutes <= 0.02)
      {
        SceneManager.LoadScene("Wait");
        break;
      }

      yield return new WaitForSeconds(1);
    }
  }

  private void Start()
  {

    // NetworkInterface.Instance.RequestHistorico("sala1");
    // NetworkInterface.Instance.RequestProximosSorteios("sala1");
    // NetworkInterface.Instance.RequestSorteioEspecial("sala1");
    // NetworkInterface.Instance.RequestSorteioSuperEspecial("sala1");
    // NetworkInterface.Instance.RequestLogos("sala1");
    NetworkInterface.Instance.RequestLogosSuperEspecial("sala1");

    if (dataAudio is null)
    {
      Debug.LogWarning("AudioData nao foi adicionado em LobbyManager");
    }

  }

  private void OnEnable()
  {

    //handler.Historico.AddListener(UpdateHistory);
    // handler.ProximoSorteio.AddListener(UpdateProximo);
    // handler.ProximosSorteios.AddListener(UpdateProximos);
    // handler.SorteioEspecial.AddListener(UpdateEspecial);
    // handler.SorteioSuperEspecial.AddListener(UpdateSuperEspecial);

    ImageCacheManager.onLogoLoad.AddListener(UpdateImageSE);

  }

  private void OnDisable()
  {
    // handler.Historico.RemoveListener(UpdateHistory);
    // handler.ProximoSorteio.RemoveListener(UpdateProximo);
    // handler.ProximosSorteios.RemoveListener(UpdateProximos);
    // handler.SorteioEspecial.RemoveListener(UpdateEspecial);
    ImageCacheManager.onLogoLoad.RemoveListener(UpdateImageSE);

    // handler.ProximosSorteios.RemoveListener(UpdateProximo);
  }

  private static string FormatTime(TimeSpan goal)
  {
    return goal.ToString("mm\\:ss");
  }
  private void UpdateImageSE(List<Logo> logo)
  {
    Debug.Log("Entrei e código SE: " + logo[0].se);


    var SE = logo[0].se;
    var dict = ImageCacheManager.Instance.Sprites;


    switch (SE)
    {
      case 1:
        var fileOng = $"{logo[1].id}.png";
        imgOng.sprite = dict[fileOng];

        var fileOferecimento = $"{logo[2].id}.png";
        imgOferecimento.sprite = dict[fileOferecimento];
        break;

      case 2:
        for (int i = 0; i < imgPartList.Count; i++)
        {
          var fileName = $"{logo[i].id}.png";
          imgPartList[i].sprite = dict[fileName];
        }
        break;

      case 3:
        for (int i = 0; i < imgSuperPartList.Count; i++)
        {
          var fileName = $"{logo[i].id}.png";
          imgSuperPartList[i].sprite = dict[fileName];

        }
        break;
    }








    // var sprites = ImageCacheManager.Instance.Sprites.Values.Take(30).ToList();
    //   for (int i = 0; i < sprites.Count; i++)
    //  {
    //   Sprite keys = sprites[i];
    //   imgPartList[i].sprite = keys;
    //   imgSuperPartList[i].sprite = keys;
    //  }

    //estou evoluindo, paciencia amigo :D
    //imgOng.sprite =  sprites[1];
    //imgOferecimento.sprite = sprites[2];

  }

  // private void UpdateEspecial(Sorteio sorteio)
  // {
  //     //atualizar variável local para imagens em cache
  //     esp = sorteio.isSpecial;

  //     if (sorteio.isSpecial)
  //     {
  //         _horaEspecial.text = sorteio.dataPartida.ToString("hh:mm") + "";
  //         _dataEspecial.text = sorteio.dataPartida.ToString("dd/MM") + "";
  //         _valorKenoEspecial.text = sorteio.valorKeno + "";
  //         _valorKinaEspecial.text = sorteio.valorKina + "";
  //         _valorKuadraEspecial.text = sorteio.valorKuadra + "";
  //     }
  // }

  // private void UpdateSuperEspecial(Sorteio sorteio)
  // {
  //     //atualizar variável local para imagens em cache
  //     espSE = sorteio.isSuperSpecial;

  //     if (sorteio.isSpecial)
  //     {
  //         _horaSuperEspecial.text = sorteio.dataPartida.ToString("hh:mm") + "";
  //         _dataSuperEspecial.text = sorteio.dataPartida.ToString("dd/MM") + "";
  //         _valorKenoSuperEspecial.text = sorteio.valorKeno + "";
  //         _valorKinaSuperEspecial.text = sorteio.valorKina + "";
  //         _valorKuadraSuperEspecial.text = sorteio.valorKuadra + "";
  //     }
  // }
  // private void UpdateProximos(List<Sorteio> proximosSorteios)
  // {
  //     for (int i = 0; i < linhasProximo.Count; i++)
  //     {
  //         linhasProximo[i].text =
  //         proximosSorteios[i].dataPartida.ToString("dd/MM") + " ÀS " +
  //         proximosSorteios[i].dataPartida.ToString("hh:mm") +
  //         " KENO: " + proximosSorteios[0].premios[2] + " " +
  //         " DOAÇÃO: " + "1.00";
  //     }

  // }
  // private void UpdateProximo(Sorteio sorteio)
  // {



  //     //Rodada Normal
  //     _valorKuadra.text = sorteio.premios[0] + "";
  //     _valorKina.text = sorteio.premios[1] + "";
  //     _valorKeno.text = sorteio.premios[2] + "";
  //     _data.text = sorteio.dataPartida.ToString("dd/MM");
  //     _hora.text = sorteio.dataPartida.ToString("hh:mm"); ;

  //     _sorteioDate = sorteio.dataPartida;
  //     _hasSorteioAtivo = true;
  //     StartCoroutine(UpdateTimer());
  // }

  // private void UpdateHistory(List<HistoryData> histData)
  // {

  //     for (int i = 0; i < linhasAnterior.Count; i++)
  //     {

  //         linhasAnterior[i].text =
  //         histData[i].dataPartida.ToString("dd/MM") + " ÀS " +
  //         histData[i].dataPartida.ToString("hh:mm") +
  //         " KENO: " + histData[0].valorKeno + " " +
  //         " DOAÇÃO: " + "1.00";
  //     }
  // }

}


[Serializable]
public class SorteioValuesUI
{
  public TextMeshProUGUI header;
  public TextMeshProUGUI kuadra;
  public TextMeshProUGUI kina;
  public TextMeshProUGUI keno;
}