using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public List<AudioClip> sounds;
    public static implicit operator AudioClip(Sound s) => s.sounds.Count > 0? s.sounds[Random.Range(0, s.sounds.Count -1)] : null;
}
