using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAreaAttack : MonoBehaviour
{
    public float areaDistance = 5f;
    public int areaDamage = 1;
    public float areaTimeBetweenAttacks = 2f;
    protected float timer;

    protected Transform playerPosition;
    protected GameObject player;
    protected PlayerHealth playerHealth;
    protected Animator animator;
    protected BaseEnemyHealth enemyHealth;
    protected PlayerMovement playerMovement;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerPosition = player.transform;

        enemyHealth = GetComponent<BaseEnemyHealth>();
        animator = GetComponent<Animator>();
    }
}
