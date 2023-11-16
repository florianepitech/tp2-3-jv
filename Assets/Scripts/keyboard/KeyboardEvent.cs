using System;
using DefaultNamespace;
using UnityEngine;

namespace keyboard
{
    public abstract class KeyboardEvent : MonoBehaviour
    {

        public static bool GetKey(KeyMovement keyMovement)
        {
            if (keyMovement == KeyMovement.None)
            {
                throw new ArgumentException("Key movement cannot be none");
            }
            var keyCode = GetKeyCode(keyMovement);
            return Input.GetKey(keyCode);
        }

        public static bool GetKeyUp(KeyMovement keyMovement)
        {
            if (keyMovement == KeyMovement.None)
            {
                throw new ArgumentException("Key movement cannot be none");
            }
            var keyCode = GetKeyCode(keyMovement);
            return Input.GetKeyUp(keyCode);
        }
        
        private static KeyCode GetKeyCode(KeyMovement keyMovement)
        {
            var value = PlayerPrefs.GetString(keyMovement.ToString());
            if (!string.IsNullOrEmpty(value))
            {
                var valueSaved = (KeyCode)Enum.Parse(typeof(KeyCode), value);
                return valueSaved;
            }
            var defaultValue = KeyDefaultValue.GetDefaultCode(keyMovement);
            return defaultValue;
        }
    
        // set deprecated
        [Obsolete]
        public static float TurnCrossHairDirectionVertical()
        {
            var result = KeyboardSettings.KeyboardType switch
            {
                KeyboardType.Azerty => TurnCrossHairVerticalAzerty(),
                KeyboardType.Qwerty => TurnCrossHairVerticalQwerty(),
                _ => throw new ArgumentOutOfRangeException()
            };
            return (result);
        }

        [Obsolete]
        public static float TurnCrossHairDirectionHorizontal()
        {
            var result = KeyboardSettings.KeyboardType switch
            {
                KeyboardType.Azerty => TurnCrossHairHorizontalAzerty(),
                KeyboardType.Qwerty => TurnCrossHairHorizontalQwerty(),
                _ => throw new ArgumentOutOfRangeException()
            };
            return (result);
        }
    
        [Obsolete]
        public static bool IsShoot()
        {
            // Verify if the player press the space bar
            return (Input.GetKeyDown(KeyCode.Space));
        }

        [Obsolete]
        public static bool IsEscape()
        {
            // Verify if the player press the escape key
            return (Input.GetKeyDown(KeyCode.Escape));
        }
    
        [Obsolete]
        public static float ArrowKeysHorizontal()
        {
            float horizontalInput = 0f;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                horizontalInput = -1f; // Left
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                horizontalInput = 1f; // Right
            }
            return horizontalInput;
        }
    
        // Vertical

        [Obsolete]
        private static float TurnCrossHairVerticalQwerty()
        {
            var verticalInput = 0f;
            if (Input.GetKey(KeyCode.W))
            {
                verticalInput = 1f; // Up
            }
            else if (Input.GetKey(KeyCode.S))
            {
                verticalInput = -1f; // Down
            }
            return verticalInput;
        }
    
        [Obsolete]
        private static float TurnCrossHairVerticalAzerty()
        {
            var verticalInput = 0f;
            if (Input.GetKey(KeyCode.Z))
            {
                verticalInput = 1f; // Up
            }
            else if (Input.GetKey(KeyCode.S))
            {
                verticalInput = -1f; // Down
            }
            return verticalInput;
        }
    
        // Horizontal
    
        [Obsolete]
        private static float TurnCrossHairHorizontalQwerty()
        {
            var horizontalInput = 0f;
        
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("Qwerty A pressed !");
                horizontalInput = -1f; // Left
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("Qwerty D pressed !");
                horizontalInput = 1f; // Right
            }

            return horizontalInput;
        }

        [Obsolete]
        private static float TurnCrossHairHorizontalAzerty()
        {
            var horizontalInput = 0f;

            if (Input.GetKey(KeyCode.Q))
            {
                Debug.Log("Azerty Q pressed !");
                horizontalInput = -1f; // Left
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("Azerty D pressed !");
                horizontalInput = 1f; // Right
            }

            return horizontalInput;
        }
     
    }
}