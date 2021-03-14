using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using UnityEngine;
using Utf8Json;
// ReSharper disable InconsistentNaming

[Serializable]
public class Sorteio
{
  //BASE INFO
  // [IgnoreDataMember]            public DateTime sorteioTime;
  [DataMember(Name = "codigo")]       public int sorteioId;
  [DataMember(Name = "data_partida")] public DateTime sorteioTime;

  //PRIZES
  [DataMember(Name = "valor_kuadra")]    public double   kuadraPrize;
  [DataMember(Name = "valor_kina")]      public double   kinaPrize;
  [DataMember(Name = "valor_keno")]      public double   kenoPrize;
  [DataMember(Name = "valor_acumulado")] public double   acumuladoPrize;
  [IgnoreDataMember]                     public double[] prizes = new double[4];

  //settings
  [DataMember(Name = "numero_bolas_acumulado")] 
    public int acumuladoBallCount; //numero de bolas para bater acumulado
  [DataMember(Name = "valor_cartela")] 
    public double donationValue;
  [IgnoreDataMember] public kenoType  type;                     //tipo de sorteio
  [IgnoreDataMember] public List<int> buyers = new List<int>(); //id de bares participantes

  //Draw
  [DataMember(Name = "bolas_sorteadas")]  public int[] balls;
  [DataMember(Name = "turnos")]  public Player[][] topPlayers;
  [DataMember(Name = "cartelas")]  public Dictionary<int, Card> cards;

  //Winners
  [DataMember(Name = "bolas_vencedoras")] public int[]   winnerBalls;
  [DataMember(Name = "vencedores")]       public int[][] winners;


  // public Sorteio()
  // {
  //   
  // }
  //
  // [Preserve]
  [SerializationConstructor]
  public Sorteio(
    int                   codigo,
    DateTime                 data_partida,
    double                valor_kuadra,
    double                valor_kina,
    double                valor_keno,
    double                valor_acumulado,
    double                valor_cartela,
    int                   numero_bolas_acumulado,
    int[]             bolas_sorteadas,
    Player[][]    turnos,
    Dictionary<int, Card> cartelas,
    int[]                 bolas_vencedoras,
    int[][]       vencedores
  )
  {
    Debug.Log("create sorteio");
    sorteioId   = codigo;
    sorteioTime = data_partida;
    cards       = cartelas;
    acumuladoBallCount = numero_bolas_acumulado;
    donationValue      = valor_cartela;
    balls              = bolas_sorteadas;
    winnerBalls        = bolas_vencedoras;
    topPlayers         = turnos;
    winners            = vencedores;
    
    prizes[0] = kuadraPrize = valor_kuadra;
    prizes[1] = kinaPrize = valor_kina;
    prizes[2] = kenoPrize = valor_keno;
    prizes[3] = acumuladoPrize = valor_acumulado;
  }


}

public enum kenoType
{
  normal,
  especial,
  superEspecial,
}

[Serializable]
public class Player{
  [DataMember(Name = "codigo")] public int id;
  [DataMember(Name = "posicao")] public int rowIndex; //0 - baixo, 1 - meio, 2 - topo, 3 - tudo/keno
  [DataMember(Name = "numeros")] public List<int> missingNumbers;
}