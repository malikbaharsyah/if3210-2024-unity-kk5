using UnityEngine;
using System.Collections;

public class BaseEnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    BaseEnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<BaseEnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        //Memindahkan posisi player
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else //Hentikan moving
        {
            nav.enabled = false;
        }
    }
}