using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
[System.Serializable]
public class Card
{
    public int codigo;
    public string estabelecimento;

    public List<int> linha1_lista;
    public List<int> linha2_lista;
    public List<int> linha3_lista;

    public int[] numbers = new int[15];

    public void Set()
    {
        numbers[0] = linha1_lista[0];
        numbers[1] = linha1_lista[1];
        numbers[2] = linha1_lista[2];
        numbers[3] = linha1_lista[3];
        numbers[4] = linha1_lista[4];

        numbers[5] = linha2_lista[0];
        numbers[6] = linha2_lista[1];
        numbers[7] = linha2_lista[2];
        numbers[8] = linha2_lista[3];
        numbers[9] = linha2_lista[4];

        numbers[10] = linha3_lista[0];
        numbers[11] = linha3_lista[1];
        numbers[12] = linha3_lista[2];
        numbers[13] = linha3_lista[3];
        numbers[14] = linha3_lista[4];
    }

}
