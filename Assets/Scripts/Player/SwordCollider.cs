using System;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public int damage = 25;
    public PlayerSwording playerSwording;

    private void OnTriggerEnter(Collider objectCollider)
    {
        if (objectCollider.CompareTag("Enemy") && playerSwording.isAttacking)
        {
            var enemyHealth = objectCollider.GetComponent<EnemyHealth>();

            if (enemyHealth)
            {
                enemyHealth.TakeDamageSword(Mathf.RoundToInt(damage));
            }
        }
    }
}
