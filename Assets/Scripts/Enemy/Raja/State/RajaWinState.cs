using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RajaWinState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- Transition to Taunting Motion --- //
        int randomValue = Random.Range(0, 3);
        switch (randomValue)
        {
            case 0:
                animator.SetTrigger("Taunt0");
                break;
            case 1:
                animator.SetTrigger("Taunt1");
                break;
            case 2:
                animator.SetTrigger("Taunt2");
                break;
        }
    }
}
