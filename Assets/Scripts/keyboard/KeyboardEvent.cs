using System;
using DefaultNamespace;
using UnityEngine;

public abstract class KeyboardEvent : MonoBehaviour
{
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
    
    // Vertical

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