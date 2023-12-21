using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerScaleWinAnimation : MonoBehaviour
{
    private Animator _animator;
    private static readonly int PlayJump = Animator.StringToHash("playJump");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var state = Game.playerTurn.Value;
        Debug.Log("state: " + state);
        if (state == 3)
        {
            _animator.SetBool(PlayJump, true);
            return;
        }
        // Cancel the animator
        _animator.SetBool(PlayJump, false);
    }
    
}
