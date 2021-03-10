using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Sorteio
{
    //BASE INFO
    public int sorteioId;
    public int sorteioTime;

    //PRIZES
    public int kuadraPrize;
    public int kinaPrize;
    public int kenoPrize;
    public int acumuladoPrize;

    //settings
    public int acumuladoBallCount; //numero de bolas para bater acumulado
    public kenoType type; //tipo de sorteio
    public List<int> buyers = new List<int>(); //id de bares participantes

    //Draw
    public List<int> balls = new List<int>();
    public List<List<player>> topPlayers = new List<List<player>>();

    //Winners
    public List<int> winnerBalls = new List<int>();
    public List<List<int>> winners = new List<List<int>>();

    public struct player
    {
        public int id;
        public int rowIndex;
    }

    public enum kenoType
    {
        normal,
        especial,
        superEspecial,
    }
}

