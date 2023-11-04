using System;
using DefaultNamespace;
using UnityEngine;

public abstract class KeyboardEvent : MonoBehaviour
{
    // TODO: Review mapping inversed for AZERTY and QWERTY
    public static float TurnCrossHairDirection()
    {
        Debug.Log("Keyboard type: " + KeyboardSettings.KeyboardType);
        var result = KeyboardSettings.KeyboardType switch
        {
            KeyboardType.Azerty => TurnCrossHairAzerty(),
            KeyboardType.Qwerty => TurnCrossHairQwerty(),
            _ => throw new ArgumentOutOfRangeException()
        };
        return (result);
    }

    public static bool IsShoot()
    {
        // Verify if the player press the space bar
        return (Input.GetKeyDown(KeyCode.Space));
    }

    public static bool IsEscape()
    {
        // Verify if the player press the escape key
        return (Input.GetKeyDown(KeyCode.Escape));
    }
    
    private static float TurnCrossHairQwerty()
    {
        var horizontalInput = 0f;
        
        if (Input.GetKey(KeyCode.Q))
        {
            horizontalInput = -1f; // Left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f; // Right
        }

        return horizontalInput;
    }

    private static float TurnCrossHairAzerty()
    {
        var horizontalInput = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = -1f; // Left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f; // Right
        }

        return horizontalInput;
    }
     
}