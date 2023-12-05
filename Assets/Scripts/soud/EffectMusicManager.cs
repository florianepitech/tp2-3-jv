using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectMusicManager : MonoBehaviour
{
    public AudioClip ShootAudioClip;
    public AudioClip GoalAudioClip;
    
    public AudioClip WinAudioClip;
    public AudioClip LoseAudioClip;
    
    private List<AudioSource> _audioSources = new();

    private void OnDestroy()
    {
        foreach (var audioSource in _audioSources)
        {
            Destroy(audioSource);
        }
    }

    private void FixedUpdate()
    {
        // Delete the audio sources that are not playing
        _audioSources.RemoveAll(audioSource => !audioSource.isPlaying);
    }

    public void PlayEffect(SoundEffect soundEffect)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        var audioClip = getAudioClip(soundEffect);
        audioSource.clip = audioClip;
        audioSource.volume = (float)MusicValue.getMusicVolume(MusicType.VFX) / 100;
        audioSource.Play();
        _audioSources.Add(audioSource);
    }

    private AudioClip getAudioClip(SoundEffect soundEffect)
    {
        switch (soundEffect)
        {
            case SoundEffect.Shoot:
                return ShootAudioClip;
            case SoundEffect.Goal:
                return GoalAudioClip;
            case SoundEffect.Win:
                return WinAudioClip;
            case SoundEffect.Loose:
                return LoseAudioClip;
            default:
                throw new ArgumentOutOfRangeException(nameof(soundEffect), soundEffect, null);
        }
    }
    
}