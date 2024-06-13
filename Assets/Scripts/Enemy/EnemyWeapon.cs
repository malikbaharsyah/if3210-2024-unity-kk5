using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int damage;

    protected GameObject player;
    protected PlayerHealth playerHealth;
    protected BaseEnemyHealth enemyHealth;
    protected Animator animator;


    virtual protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponentInParent<BaseEnemyHealth>();
        animator = GetComponentInParent<Animator>();
    }
}
