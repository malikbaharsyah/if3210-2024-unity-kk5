using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChaseState : BaseState
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- Transition to idle state --- //
        if (playerHealth.currentHealth <= 0)
        {
            animator.SetTrigger("PlayerDead");
        }
        else
        {
            // --- Transition to attack state --- // 
            float distanceFromPlayer = Vector3.Distance(playerPosition.position, animator.transform.position);
            if (distanceFromPlayer < attackingDistance)
            {
                animator.SetBool("isAttacking", true);
            }
        }
    }
}