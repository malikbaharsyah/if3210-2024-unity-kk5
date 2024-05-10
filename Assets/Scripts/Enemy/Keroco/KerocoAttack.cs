//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class KerocoAttack : EnemyAttack
//{
//    protected override void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject == player && other.isTrigger == false)
//        {
//            anim.SetBool("isAttacking", false);
//            playerInRange = false;
//        }
//    }


//    protected override void Update()
//    {
//        timer += Time.deltaTime;

//        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
//        {
//            Attack();
//        }

//        if (playerHealth.currentHealth <= 0)
//        {
//            anim.SetTrigger("PlayerDead");
//            anim.SetBool("isAttacking", false);
//        }
//    }


//    protected override void Attack()
//    {
//        timer = 0f;

//        if (playerHealth.currentHealth > 0)
//        {
//            anim.SetBool("isAttacking", true);
//            playerHealth.TakeDamage(attackDamage);
//        }
//    }
//}
