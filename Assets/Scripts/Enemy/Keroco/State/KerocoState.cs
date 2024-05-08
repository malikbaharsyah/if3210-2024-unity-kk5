using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KerocoState : StateMachineBehaviour
{
    public float attackingDistance = 1.1f;

    protected Transform playerPosition;
    protected GameObject player;
    protected PlayerHealth playerHealth;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerPosition = player.transform;
    }
}
