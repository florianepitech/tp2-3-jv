using System;
using Unity.Netcode;

namespace DefaultNamespace
{
    
    public struct PlayerScore
    {
        public ulong PlayerId;
        public int Score;
    }
    public class ObstacleManager : NetworkBehaviour
    {
        public static NetworkVariable<PlayerScore[]> PlayerScores;
        
    }
}