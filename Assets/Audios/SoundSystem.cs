using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundSystem : MonoBehaviour
{
    private static SoundSystem _instance;
    [SerializeField] private AudioSource VoiceSource;
    [SerializeField] private AudioSource BallSource;
    [SerializeField] private AudioSource SFXsource;


    private void OnValidate()
    {
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }

        _instance = this;
    }



    public static void Play(AudioClip clip, AudioType type, float volume = 1f, AudioMixerGroup mixer = null)
    {
        AudioSource source = null;
        if (type == AudioType.Ball) source = _instance.BallSource;
        else if (type == AudioType.SFX) source = _instance.SFXsource;
        else if (type == AudioType.Voice) source = _instance.VoiceSource;

        source.volume = volume;
        source.clip = clip;
        source.outputAudioMixerGroup = mixer;
        source.Play();
    }
}