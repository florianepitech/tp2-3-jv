using UnityEngine;

public class KeyboardKeySaver
{
    public static void saveKey(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
}