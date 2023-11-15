using UnityEngine;

public class KeyManager: MonoBehaviour
{
    public string keyName = "";
    
    void Update()
    {
        var lastKeyPressed = GetLastKeyPressed();
        if (lastKeyPressed != KeyCode.None)
        {
            SaveKey(keyName, lastKeyPressed.ToString());
        }
    }
    
    private static KeyCode GetLastKeyPressed()
    {
        var lastKeyPressed = KeyCode.None;
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                lastKeyPressed = keyCode;
            }
        }

        return lastKeyPressed;
    }
    
    private static void SaveKey(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    
}