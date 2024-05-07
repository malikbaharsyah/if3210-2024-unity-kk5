using System;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public int damage = 25;
    // private bool oneHit;
    public PlayerSwording playerSwording;

    // private void Awake()
    // {
    //     oneHit = false;
    // }

    private void OnTriggerEnter(Collider objectCollider)
    {
        if (objectCollider.CompareTag("Enemy") && playerSwording.isAttacking)
        {
            var enemyHealth = objectCollider.GetComponent<EnemyHealth>();

            if (enemyHealth)
            {
                // if (oneHit)
                // {
                //     enemyHealth.TakeDamageSword(Mathf.RoundToInt(enemyHealth.currentHealth));
                // }
                // else
                // {
                    enemyHealth.TakeDamageSword(Mathf.RoundToInt(damage));
                // }
            }
        }
    }

    // public void SetOneHit() // Cheat
    // {
    //     oneHit = true;
    // }
}
