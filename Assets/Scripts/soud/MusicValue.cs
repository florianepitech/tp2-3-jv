using System.Collections.Generic;
using UnityEngine;

public class MusicValue
{
    // Cache the values to avoid PlayerPrefs.GetString calls
    private static Dictionary<MusicType, int> cache = new();
    
    public static int getMusicVolume(MusicType musicType)
    {
        if (cache.ContainsKey(musicType))
        {
            return cache[musicType];
        }
        var result = PlayerPrefs.GetInt(musicType.ToString());
        cache.Add(musicType, result);
        return result;
    }
    
    public static void setMusicVolume(MusicType musicType, int volume)
    {
        cache[musicType] = volume;
        PlayerPrefs.SetInt(musicType.ToString(), volume);
    }
    
}