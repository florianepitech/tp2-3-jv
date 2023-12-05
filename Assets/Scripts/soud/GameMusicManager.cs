using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameMusicManager : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    
    private AudioSource track1, track2;
    private bool isTrack1Playing = true;

    private static GameMusicManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Track 1
        track1 = gameObject.AddComponent<AudioSource>();
        track1.loop = true;
        track1.volume = (float)MusicValue.getMusicVolume(MusicType.Music) / 100;
        track2 = gameObject.AddComponent<AudioSource>();
        // Track 2
        track2.loop = true;
        track2.volume = (float)MusicValue.getMusicVolume(MusicType.Music) / 100;
        
        isTrack1Playing = true;

        SwapTrack();
    }
    
    public void SwapTrack()
    {
        var newTrack = isTrack1Playing ? clip2 : clip1;
        StopAllCoroutines();
        StartCoroutine(FadeOut(newTrack));
        isTrack1Playing = !isTrack1Playing;
    }

    public void ReturnToDefault()
    {
        SwapTrack();
    }
    
    private IEnumerator FadeOut(AudioClip newClip)
    {
        float timeToFade = 1f;
        float timeElapsed = 0f;
        var volume = (float)MusicValue.getMusicVolume(MusicType.Music) / 100;

        if (isTrack1Playing)
        {
            track2.clip = newClip;
            track2.Play();

            while (timeElapsed < timeToFade)
            {
                track2.volume = Mathf.Lerp(0f, volume, timeElapsed / timeToFade);
                track1.volume = Mathf.Lerp(volume, 0f, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            track1.Stop();
        }
        else
        {
            track1.clip = newClip;
            track1.Play();

            while (timeElapsed < timeToFade)
            {
                track1.volume = Mathf.Lerp(0f, volume, timeElapsed / timeToFade);
                track2.volume = Mathf.Lerp(volume, 0f, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;   
            }
            track2.Stop();
        }
    }
    
}