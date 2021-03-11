using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

// ReSharper disable InconsistentNaming

[CreateAssetMenu(menuName = "EventHandler")]
public class EventHandler : ScriptableObject
{
  //numero puxado
  // public readonly NumberEvent number = new NumberEvent();

  // public readonly NumberEvent visualNumber = new NumberEvent();


  //ganhou
  // public readonly VerifyEvent    VerifyWin  = new VerifyEvent();
  // public readonly WinEvent       Win        = new WinEvent();
  //public readonly WinVisualEvent ShowWinner = new WinVisualEvent();

  //Sorteio
  public readonly UnityEvent<Sorteio> SorteioResultado = new UnityEvent<Sorteio>();

  //Historico
  public readonly UnityEvent<Sorteio> ProximoSorteio = new UnityEvent<Sorteio>();
  public readonly UnityEvent<Sorteio> SorteioEspecial = new UnityEvent<Sorteio>();
  public readonly UnityEvent<Sorteio> SorteioSuperEspecial = new UnityEvent<Sorteio>();
  public readonly UnityEvent<List<Sorteio>> ProximosSorteios = new UnityEvent<List<Sorteio>>();
  //public readonly UnityEvent<List<HistoryData>> Historico = new UnityEvent<List<HistoryData>>();
  public readonly UnityEvent<List<Logo>> LogoUrlOngOferecimento = new UnityEvent<List<Logo>>();
  public readonly UnityEvent<List<Logo>> LogoUrlEspecial = new UnityEvent<List<Logo>>();
  public readonly UnityEvent<List<Logo>> LogoUrlSuperEspecial = new UnityEvent<List<Logo>>();
  public readonly UnityEvent<string> Ong = new UnityEvent<string>();
  public readonly UnityEvent<List<string>> Participantes = new UnityEvent<List<string>>();
  public readonly UnityEvent OnReplay = new UnityEvent();
}

// public class NumberEvent : UnityEvent<int>
// {
// }
//
// public class WinEvent : UnityEvent<List<Cartelado>, int>
// {
// }
//
// public class VerifyEvent : UnityEvent<Cartelado, int>
// {
// }



// public class SorteioEvent : UnityEvent<Sorteio>
// {
// }



// public class FontEvent : UnityEvent<List<TMP_FontAsset>>
// {
// }