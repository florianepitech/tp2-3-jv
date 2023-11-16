using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace keyboard
{
    public class KeyManager : MonoBehaviour
    {
        public KeyMovement keyMovement = KeyMovement.None;
    
        private TMP_InputField _inputField;

        void Start()
        {
            _inputField = GetComponent<TMP_InputField>();
            if (keyMovement == KeyMovement.None)
            {
                _inputField.text = "Error...";
                Debug.LogError("Key movement is null");
                return;
            }
            if (_inputField == null)
            {
                Debug.LogError("TMP input field not found");
            }
            var savedKey = GetSavedKey(keyMovement.ToString());
            if (savedKey != KeyCode.None)
            {
                _inputField.text = savedKey.ToString();
            }
            else
            {
                var defaultValue = KeyDefaultValue.GetDefaultCode(keyMovement);
                _inputField.text = defaultValue.ToString();
            }
        }
    
        void Update()
        {
            if (keyMovement == KeyMovement.None)
            {
                return;
            }
            if (!_inputField.isFocused)
            {
                return;
            }
            var lastKeyPressed = GetLastKeyPressed();
            if (lastKeyPressed != KeyCode.None)
            {
                SaveKey(keyMovement.ToString(), lastKeyPressed);
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