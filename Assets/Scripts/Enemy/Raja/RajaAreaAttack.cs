using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RajaAreaAttack : BaseAreaAttack
{
    public float speedChangeFactor = 1f;
    bool isSlowed = false;

    void Update()
    {
        timer += Time.deltaTime;
        float distanceFromPlayer = Vector3.Distance(playerPosition.position, animator.transform.position);

        if (distanceFromPlayer <= areaDistance && enemyHealth.currentHealth > 0)
        {
            if (!isSlowed)
            {
                isSlowed = true;
                playerMovement.speed /= speedChangeFactor;
            }

            if (timer  >= areaTimeBetweenAttacks)
            {
                playerHealth.TakeDamage(areaDamage);
                timer = 0f;
            }
        }
        else
        {
            if (isSlowed)
            {
                isSlowed = false;
                playerMovement.speed *= speedChangeFactor;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, areaDistance);
    }
}
