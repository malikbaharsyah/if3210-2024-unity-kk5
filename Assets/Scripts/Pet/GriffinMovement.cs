using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GriffinMovement : MonoBehaviour
{
    public float speed = 5f;
    public float followDistance = 5f;
    public int healingAmount = 20;
    PlayerHealth playerHealth;
    NavMeshAgent agent;
    Transform player;
    Transform pet;
    PetHealth petHealth;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        petHealth = GetComponent<PetHealth>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed = speed;
        InvokeRepeating("Heal", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (petHealth.currentHealth > 0 && player != null)
        {
            Vector3 movePosition = player.position;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Avoid enemies
            Collider[] enemies = Physics.OverlapSphere(transform.position, 10f); 
            foreach (var enemy in enemies)
            {
                if (enemy.CompareTag("Enemy")) 
                {
                    Vector3 awayFromEnemy = transform.position - enemy.transform.position;
                    movePosition += awayFromEnemy;
                }
            }

            if (distanceToPlayer > followDistance)
            {
                agent.SetDestination(movePosition);
                anim.SetBool("IsFlying", true);
            }
            else
            {
                agent.ResetPath();
                anim.SetBool("IsFlying", false);
            }
        }
        else
        {
            anim.SetBool("IsDead", true);
            agent.enabled = false;
        }
    }


    void Heal()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healingAmount);
        }
    }

    void DisableHealing()
    {
        CancelInvoke("Heal");
    }

}
