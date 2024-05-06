using System.Diagnostics;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    GameObject pet;
    PlayerHealth playerHealth;
    PetHealth petHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    bool petInRange;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                UnityEngine.Debug.LogError("Missing PlayerHealth component on player object.");
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Player object not found.");
        }

        pet = GameObject.FindGameObjectWithTag("PlayerPet");
        if (pet != null)
        {
            petHealth = pet.GetComponent<PetHealth>();
            if (petHealth == null)
            {
                UnityEngine.Debug.LogError("Missing PetHealth component on pet object.");
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Pet object not found.");
        }

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            UnityEngine.Debug.LogError("Missing Animator component on enemy.");
        }

        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth == null)
        {
            UnityEngine.Debug.LogError("Missing EnemyHealth component on enemy.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !other.isTrigger)
        {
            playerInRange = true;
        }
        else if (other.gameObject == pet && !other.isTrigger)
        {
            petInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && !other.isTrigger)
        {
            playerInRange = false;
        }
        if (other.gameObject == pet && !other.isTrigger)
        {
            petInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (enemyHealth.currentHealth > 0 && timer >= timeBetweenAttacks)
        {
            if (playerInRange && playerHealth != null && playerHealth.currentHealth > 0)
            {
                Attack(playerHealth);
            }
            else if (petInRange && petHealth != null && petHealth.currentHealth > 0)
            {
                Attack(petHealth);
            }
        }

        if (playerHealth != null && playerHealth.currentHealth <= 0 && (petHealth == null || petHealth.currentHealth <= 0))
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    void Attack(PlayerHealth targetHealth)
    {
        timer = 0f;
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(attackDamage);
        }
        else
        {
            UnityEngine.Debug.LogError("Attempted to attack a target with no health component.");
        }
    }

    void AttackPet()
    {
        timer = 0f;

        // Taking damage
        if (petHealth.currentHealth > 0)
        {
            petHealth.TakeDamage(attackDamage);
        }
    }
}
