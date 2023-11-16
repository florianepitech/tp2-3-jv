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
                KeyMovement.CrossHairLeft => KeyCode.Z,
                KeyMovement.CrossHairUp => KeyCode.E,
                KeyMovement.CrossHairDown => KeyCode.R,
                KeyMovement.Shoot => KeyCode.T,
                KeyMovement.CameraRight => KeyCode.Y,
                KeyMovement.CameraLeft => KeyCode.U,
                KeyMovement.PauseMenu => KeyCode.I,
                _ => throw new ArgumentOutOfRangeException(nameof(keyMovement), keyMovement, null)
            };
        }
        
    }
}