using keyboard;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace.Movements
{
    public class ShootBarRotations : NetworkBehaviour
    {
        void Update() 
        {
            if (IsLocalPlayer) 
            {
                var horizontalRotation = TurnCrossHairDirectionHorizontal();
                var verticalRotation = TurnCrossHairDirectionVertical();

                // Send these values to the server to update the shoot bar's rotation
                //SendRotationInputToServer(horizontalRotation, verticalRotation);
            }
        }
        
        private int TurnCrossHairDirectionHorizontal()
        {
            var result = 0;
            if (KeyboardEvent.GetKey(KeyMovement.CrossHairLeft))
                result += 1;
            if (KeyboardEvent.GetKey(KeyMovement.CrossHairRight))
                result -= 1;
            return result;
        }
    
        private int TurnCrossHairDirectionVertical()
        {
            var result = 0;
            if (KeyboardEvent.GetKey(KeyMovement.CrossHairUp))
                result += 1;
            if (KeyboardEvent.GetKey(KeyMovement.CrossHairDown))
                result -= 1;
            return result;
        }
    }
}