using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ObstacleTrigger : NetworkBehaviour
{
    // Start is called before the first frame update
    bool passed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
            
            if (other.gameObject.CompareTag("Player"))
            {
                if (passed)
                {
                    return;
                }
                Game.passedObstacles++;
                passed = true;
            }
    }
}
