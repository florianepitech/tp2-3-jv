using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace keyboard
{
    public class KeyManager : MonoBehaviour
    {
        public string keyName;
        
        public KeyCode defaultValue;
    
        private TMP_InputField _inputField;

        void Start()
        {
            _inputField = GetComponent<TMP_InputField>();
            if (_inputField == null)
            {
                Debug.LogError("TMP input field not found");
            }
            var savedKey = GetSavedKey(keyName);
            if (savedKey != KeyCode.None)
            {
                _inputField.text = savedKey.ToString();
            }
            else if (defaultValue != KeyCode.None)
            {
                _inputField.text = defaultValue.ToString();
            }
        }
    
        void Update()
        {
            if (!_inputField.isFocused)
            {
                return;
            }
            var lastKeyPressed = GetLastKeyPressed();
            if (lastKeyPressed != KeyCode.None)
            {
                SaveKey(keyName, lastKeyPressed);
                _inputField.text = lastKeyPressed.ToString();
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
    
        private static void SaveKey(string key, KeyCode value)
        {
            if (key == null)
            {
                Debug.LogError("Key is null");
                return;
            }
            PlayerPrefs.SetString(key, value.ToString());
        }
    
        private static KeyCode GetSavedKey(string key)
        {
            if (key == null)
            {
                Debug.LogError("Key is null");
                return KeyCode.None;
            }
            var code = PlayerPrefs.GetString(key);
            return code == "" ?
                KeyCode.None : (KeyCode) System.Enum.Parse(typeof(KeyCode), code);
        }
        
    }
}