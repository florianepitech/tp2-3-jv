using System;
using UnityEngine;

public class SoudBoxManager
{
    
    public static void playSoundEffect(GameObject gameObject, SoundEffect soundEffect)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        
        audioSource.Play();
    }
    
    /*
    private readonly string[] _menuScene = new string[] {"HomeMenu", "KeyboardSettings", "JoinGame", "StartScreen"};
    
    private readonly string[] _gameScene = new string[] {"Game", "PauseMenu"};

    private string _actualScene = null;
    
    public AudioSource shootEffect;
    public AudioSource goalEffect;
    public AudioSource winEffect;
    public AudioSource loseEffect;
    
    public AudioSource menuMusic;
    public AudioSource gameMusic;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
    }

    private void FixedUpdate()
    {
        // Get the actual scene
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        // Check if the scene is the menu
        if (isGameScene())
        {
            playGameMusic();
            return;
        }
        playMenuMusic();
        _actualScene = scene.name;
    }

    private void playMenuMusic()
    {
        Debug.Log("Play menu music");
    }

    private void playGameMusic()
    {
        Debug.Log("Play game music");
    }

    public void playSoundEffect(SoundEffect soundEffect)
    {
        var audioSource = getAudioSource(soundEffect);
        audioSource.Play();
    }
    
    private bool isGameScene()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        foreach (var s in _gameScene)
        {
            if (s == scene.name)
                return true;
        }

        return (false);
    }

    private AudioSource getAudioSource(SoundEffect soundEffect)
    {
        switch (soundEffect)
        {
            case SoundEffect.Shoot:
                return shootEffect;
            case SoundEffect.Goal:
                return goalEffect;
            case SoundEffect.Win:
                return winEffect;
            case SoundEffect.Loose:
                return loseEffect;
            default:
                throw new ArgumentOutOfRangeException(nameof(soundEffect), soundEffect, null);
        }
        
    }
    */
}