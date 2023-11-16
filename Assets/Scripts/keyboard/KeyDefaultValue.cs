using System;
using UnityEngine;

namespace keyboard
{
    public abstract class KeyDefaultValue
    {

        public static KeyCode GetDefaultCode(KeyMovement keyMovement)
        {
            return keyMovement switch
            {
                KeyMovement.CrossHairRight => KeyCode.A,
                KeyMovement.CrossHairLeft => KeyCode.D,
                KeyMovement.CrossHairUp => KeyCode.W,
                KeyMovement.CrossHairDown => KeyCode.S,
                KeyMovement.Shoot => KeyCode.Space,
                KeyMovement.CameraRight => KeyCode.RightArrow,
                KeyMovement.CameraLeft => KeyCode.LeftArrow,
                KeyMovement.PauseMenu => KeyCode.Escape,
                _ => throw new ArgumentOutOfRangeException(nameof(keyMovement), keyMovement, null)
            };
        }
        
    }
}