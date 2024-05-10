using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RajaAttackState : BaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerPosition = player.transform;

        if (playerHealth.currentHealth > 0 && (animator.GetBool("isSpawningKeroco") == false))
        {
            // --- Do Attack Motion --- //
            int randomValue = Random.Range(0, 4);
            switch (randomValue)
            {
                case 0:
                    animator.SetTrigger("Attack0");
                    break;
                case 1:
                    animator.SetTrigger("Attack1");
                    break;
                case 2:
                    animator.SetTrigger("Attack2");
                    break;
                case 3:
                    animator.SetTrigger("Attack3");
                    break;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- Transition to idle state --- //
        if (playerHealth.currentHealth <= 0)
        {
            animator.SetBool("isAttacking", false);
            animator.SetTrigger("PlayerDead");
        }
        else
        {
            // --- Transition to chase state --- // 
            float distanceFromPlayer = Vector3.Distance(playerPosition.position, animator.transform.position);
            if (distanceFromPlayer >= attackingDistance)
            {
                animator.SetBool("isAttacking", false);
            }
        }
    }
}
