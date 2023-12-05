using System;
using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip audioClip;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = audioClip;
        _audioSource.loop = true;
        _audioSource.volume = (float)MusicValue.getMusicVolume(MusicType.Music) / 100;
    }
    
    private void FixedUpdate()
    {
        // Adapt the volume to the slider value
        _audioSource.volume = (float)MusicValue.getMusicVolume(MusicType.Music) / 100;
        // If the scene is game, not play the music
        if (IsGameScene())
        {
            _audioSource.volume = 0;
            return;
        }
        // If the music is not playing, play it
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
    
    private bool IsGameScene()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        return (scene.name == "Game");
    }
}