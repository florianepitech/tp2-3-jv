using DefaultNamespace;
using UnityEngine;

public class KeyboardSettings : MonoBehaviour
{
    public static KeyboardType KeyboardType { get; private set; } = KeyboardType.QWERTY;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Keyboard settings loaded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnBackButtonPressed()
    {
        Debug.Log("Keyboard settings back button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeMenu");
    }
 
    public void OnKeyboardSettingsChanged(int index)
    {
        Debug.Log("Keyboard settings changed to " + index);
        switch (index)
        {
            case 0:
                KeyboardType = KeyboardType.QWERTY;
                break;
            case 1:
                KeyboardType = KeyboardType.AZERTY;
                break;
            default:
                Debug.LogError("Unknown keyboard type");
                break;
        }
    }
}
