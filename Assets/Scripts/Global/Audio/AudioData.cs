using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Audios")]

public class AudioData : ScriptableObject
{
  [Header("Listas")]

  
  public List<Sound> numbers;
  public List<Sound> prizes;
  public List<Sound> calls;

  [Header("Listas")]
  public Sound sfxSaidaBola;
  public Sound sfxSaidaKeno;
  public Sound sfxBola;
  public Sound sfxVitora;
  //adicionar audio das 90 bolas no inspector
   //public List<AudioClip> NumberAudio;
  // private void OnValidate()
  // {
  //   for (int i1 = 0; i1 < NumberAudio.Count; i1++)
  //   {
  //     AudioClip i = NumberAudio[i1];
  //     if (i1 >= numbers.Count) break;
  //     numbers[i1].sounds = new List<AudioClip>() { i };
  //   }
  // }

}

