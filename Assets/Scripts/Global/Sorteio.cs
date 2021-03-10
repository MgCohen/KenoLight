using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Newtonsoft.Json;
using static System.Globalization.DateTimeStyles;

// ReSharper disable InconsistentNaming

public class Sorteio
{
    //BASE INFO
    public int sorteioId;
    public DateTime sorteioTime;

    //PRIZES
    public readonly double kuadraPrize;
    public readonly double kinaPrize;
    public readonly double kenoPrize;
    public readonly double acumuladoPrize;
    public readonly double[] prizes = new double[4];

    //settings
    public readonly int acumuladoBallCount; //numero de bolas para bater acumulado
    public readonly double donationValue;
    public readonly kenoType type;                     //tipo de sorteio
    public readonly List<int> buyers = new List<int>(); //id de bares participantes

    //Draw
    public readonly List<int> balls;
    public readonly List<List<player>> topPlayers = new List<List<player>>();
    public readonly List<Card> cards;

    //Winners
    public readonly int[] winnerBalls = new int[3];
    public readonly List<List<int>> winners = new List<List<int>>();

    public struct player
    {
        public player(int newId)
        {
            id = 9650 + newId;
            //rowIndex = row;
            rowIndex = 0;
        }

        public int id;
        public int rowIndex; //0 - baixo, 1 - meio, 2 - topo, 3 - tudo/keno
    }

    [JsonConstructor]
    public Sorteio(
        bool replay,
        bool especial,
        bool superEspecial,
        int codigo,
        string data_partida,
        double valor_kuadra,
        double valor_kina,
        double valor_keno,
        double valor_acumulado,
        double valor_cartela,
        List<Card> cartelas,
        string bolas_sorteadas,
        int numero_bolas_acumulado,
        int bola_kuadra,
        int bola_kina,
        int bola_keno
    )
    {
        sorteioId = codigo;
        sorteioTime = data_partida != null
                          ? DateTime.Parse(data_partida, null, RoundtripKind).ToLocalTime()
                          : DateTime.Now.AddSeconds(30);


        cards = cartelas;

        //TEST
        foreach (var card in cards) card.Set();
        //REMOVE

        prizes[0] = kuadraPrize = valor_kuadra;
        prizes[1] = kinaPrize = valor_kina;
        prizes[2] = kenoPrize = valor_keno;
        prizes[3] = acumuladoPrize = valor_acumulado;

        winnerBalls[0] = bola_kuadra;
        winnerBalls[1] = bola_kina;
        winnerBalls[2] = bola_keno;

        acumuladoBallCount = numero_bolas_acumulado;
        //numeroBolasAcumulado = 90;
        donationValue = valor_cartela;
        // replay       = replay;


        balls = !string.IsNullOrEmpty(bolas_sorteadas)
            ? bolas_sorteadas.Split(',').Select(int.Parse).ToList()
            : new List<int>();

        if (cartelas != null) Debug.Log(cartelas.Count);

        winners.Add(new List<int>() { 9652 });
        winners.Add(new List<int>() { 9652 });
        winners.Add(new List<int>() { 9650 });

        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(2), new player(3), new player(4) });
        topPlayers.Add(new List<player>() { new player(1), new player(0), new player(4), new player(2), new player(3) });
        topPlayers.Add(new List<player>() { new player(2), new player(1), new player(0), new player(4), new player(3) });
        topPlayers.Add(new List<player>() { new player(2), new player(1), new player(3), new player(0), new player(4) });
        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(2), new player(3), new player(4) });
        topPlayers.Add(new List<player>() { new player(1), new player(0), new player(2), new player(3), new player(4) });
        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(2), new player(3), new player(4) });
        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(4), new player(3), new player(2) });
        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(2), new player(3), new player(4) });
        topPlayers.Add(new List<player>() { new player(0), new player(2), new player(3), new player(1), new player(4) });
        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(2), new player(3), new player(4) });
        topPlayers.Add(new List<player>() { new player(4), new player(1), new player(2), new player(3), new player(0) });
        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(2), new player(3), new player(4) });
        topPlayers.Add(new List<player>() { new player(2), new player(3), new player(0), new player(1), new player(4) });
        topPlayers.Add(new List<player>() { new player(1), new player(3), new player(2), new player(0), new player(4) });
        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(2), new player(4), new player(3) });
        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(2), new player(3), new player(4) });
        topPlayers.Add(new List<player>() { new player(0), new player(1), new player(2), new player(3), new player(4) });
    }


}

public enum kenoType
{
    normal,
    especial,
    superEspecial,
}

