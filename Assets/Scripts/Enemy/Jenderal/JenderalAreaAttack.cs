using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JenderalAreaAttack : BaseAreaAttack
{
    void Update()
    {
        timer += Time.deltaTime;
        float distanceFromPlayer = Vector3.Distance(playerPosition.position, animator.transform.position);
        if (distanceFromPlayer <= areaDistance && timer >= areaTimeBetweenAttacks && enemyHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(areaDamage);
            timer = 0f;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaDistance);
    }
}
