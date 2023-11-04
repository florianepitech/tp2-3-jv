using DefaultNamespace;
using UnityEngine;

public class KeyboardSettings : MonoBehaviour
{
    public static KeyboardType KeyboardType { get; private set; } = KeyboardType.Qwerty;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Keyboard settings loaded");
    }
    
    public void OnBackButtonPressed()
    {
        Debug.Log("Keyboard settings back button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeMenu");
    }
 
    public void OnKeyboardSettingsChanged(int index)
    {
        Debug.Log("Last value is " + KeyboardType);
        switch (index)
        {
            case 0:
                KeyboardType = KeyboardType.Qwerty;
                break;
            case 1:
                KeyboardType = KeyboardType.Azerty;
                break;
            default:
                Debug.LogError("Unknown keyboard type");
                break;
        }
        Debug.Log("Keyboard settings changed to " + index + " " + KeyboardType);
    }
}
