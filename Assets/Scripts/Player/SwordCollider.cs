using System;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public int damage = 25;
    private float _multiplier;
    private bool oneHit;
    public PlayerSwording playerSwording;

    private void Awake()
    {
        _multiplier = 1;
        oneHit = false;
    }

    private void OnTriggerEnter(Collider objectCollider)
    {
        if (objectCollider.CompareTag("Enemy") && playerSwording.isAttacking)
        {
            var enemyHealth = objectCollider.GetComponent<EnemyHealth>();

            if (enemyHealth)
            {
                if (oneHit)
                {
                    enemyHealth.TakeDamageSword(Mathf.RoundToInt(enemyHealth.currentHealth));
                }
                else
                {
                    _multiplier = 1;
                    enemyHealth.TakeDamageSword(Mathf.RoundToInt(damage * _multiplier));
                }
            }
        }
    }

    public void SetOneHit() // Cheat
    {
        oneHit = true;
    }
}
