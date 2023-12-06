using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectMusicManager : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.clip = audioClip;
        _audioSource.volume = (float)MusicVolume.getMusicVolume(MusicType.VFX) / 100;
    }

    public void Play()
    {
        _audioSource.Play();
    }

    // private AudioClip GetAudioClip(SoundEffect soundEffect)
    // {
    //     switch (soundEffect)
    //     {
    //         case SoundEffect.Shoot:
    //             return ShootAudioClip;
    //         case SoundEffect.Goal:
    //             return GoalAudioClip;
    //         case SoundEffect.Win:
    //             return WinAudioClip;
    //         case SoundEffect.Loose:
    //             return LoseAudioClip;
    //         default:
    //             throw new ArgumentOutOfRangeException(nameof(soundEffect), soundEffect, null);
    //     }
    // }
    
}