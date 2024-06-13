using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : MonoBehaviour
{
    public int damage;

    private GameObject player;
    private PlayerHealth playerHealth;
    private BaseEnemyHealth enemyHealth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponentInParent<BaseEnemyHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (
            other.gameObject == player && 
            other.isTrigger == false && 
            playerHealth.currentHealth > 0 && 
            enemyHealth.currentHealth > 0
            )
        {
            playerHealth.TakeDamage(damage);
        }
    }
}