using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public AudioMixerGroup mixerGroup;
    public AudioData data;



    public static void PlayNumberVoice(int number)
    {
        var index = number - 1;
        if (index < 0 || index >= instance.data.numbers.Count) return;

        var clip = instance.data.numbers[index];
        SoundSystem.Play(clip, AudioType.Voice, 1, instance.mixerGroup);

    }
    public static void EntryNumber()
    {
        SoundSystem.Play(instance.data.sfxBola, AudioType.Ball, 1, instance.mixerGroup);
    }
    public static void ExitNumber()
    {
        SoundSystem.Play(instance.data.sfxSaidaBola, AudioType.Ball, 1, instance.mixerGroup);
    }
    public static void callPrize(int prize)
    {
        var index = (prize) - 4;
        if (index < 0 || index >= instance.data.prizes.Count) return;

        var clip = instance.data.prizes[index];

        SoundSystem.Play(clip, AudioType.Voice, 1f, instance.mixerGroup);

    }
}

public enum AudioType
{
    Voice,
    Ball,
    SFX
}