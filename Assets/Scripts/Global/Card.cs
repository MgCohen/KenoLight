using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Scripting;
// ReSharper disable InconsistentNaming

// [Preserve]
[System.Serializable]
public class Card
{
   [DataMember(Name = "codigo")]public int codigo;
   [DataMember(Name = "estabelecimento")]public string estabelecimento;
   
   [DataMember(Name = "linha1_lista")]public int[] linha1;
   [DataMember(Name = "linha2_lista")]public int[] linha2;
   [DataMember(Name = "linha3_lista")]public int[] linha3;

   [IgnoreDataMember]public int[] numbers = new int[15];

    public Card(
         int codigo,
         string estabelecimento,
         int[] linha1_lista,
         int[] linha2_lista,
         int[] linha3_lista
       )
    {
      
      linha1               = linha1_lista; 
      linha2               = linha2_lista;
      linha3               = linha3_lista;
      this.codigo          = codigo;
      this.estabelecimento = estabelecimento;
      
      numbers[0] = linha1[0];
      numbers[1] = linha1[1];
      numbers[2] = linha1[2];
      numbers[3] = linha1[3];
      numbers[4] = linha1[4];
      
      numbers[5] = linha2[0];
      numbers[6] = linha2[1];
      numbers[7] = linha2[2];
      numbers[8] = linha2[3];
      numbers[9] = linha2[4];
    
      numbers[10] = linha3[0];
      numbers[11] = linha3[1];
      numbers[12] = linha3[2];
      numbers[13] = linha3[3];
      numbers[14] = linha3[4];
    }

}
