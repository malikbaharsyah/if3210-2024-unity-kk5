using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    Transform pet;
    GameObject playerPet;
    PlayerHealth playerHealth;
    PetHealth petHealth; // Updated to use PetHealth instead of PlayerHealth for pet
    EnemyHealth enemyHealth;
    NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        pet = GameObject.FindGameObjectWithTag("PlayerPet") ? GameObject.FindGameObjectWithTag("PlayerPet").transform : null;
        if (pet != null)
        {
            petHealth = pet.GetComponent<PetHealth>();
        }
        if (enemyHealth.currentHealth > 0)
        {
            if (playerHealth.currentHealth > 0 && (petHealth == null || petHealth.currentHealth <= 0))
            {
                // Only player is alive and valid target
                nav.SetDestination(player.position);
            }
            else if (petHealth != null && petHealth.currentHealth > 0 && (playerHealth.currentHealth <= 0))
            {
                // Only pet is alive and valid target
                nav.SetDestination(pet.position);
            }
            else if (playerHealth.currentHealth > 0 && petHealth != null && petHealth.currentHealth > 0)
            {
                // Both are valid targets, choose the closest one
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                float distanceToPet = Vector3.Distance(transform.position, pet.position);

                if (distanceToPlayer < distanceToPet)
                {
                    nav.SetDestination(player.position);
                }
                else
                {
                    nav.SetDestination(pet.position);
                }
            }
        }
        else
        {
            // Enemy is dead or incapacitated, disable navigation
            nav.enabled = false;
        }
    }
}
