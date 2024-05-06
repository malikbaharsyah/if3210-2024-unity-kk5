using UnityEngine;
using System.Collections;

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
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        // Mendapatkan Enemy health
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Callback jika ada suatu object masuk ke dalam trigger
    void OnTriggerEnter(Collider other)
    {
        // Set player in range
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;

        }
        // Set pet in range
        if (other.gameObject == pet && other.isTrigger == false)
        {
            petInRange = true;
        }
    }

    // Callback jika ada object yang keluar dari trigger
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = false;
        }
        if (other.gameObject == pet && other.isTrigger == false)
        {
            petInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        pet = GameObject.FindGameObjectWithTag("PlayerPet");
        if (pet != null)
        {
            petHealth = pet.GetComponent<PetHealth>();
        }

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (timer >= timeBetweenAttacks && petInRange && enemyHealth.currentHealth > 0)
        {
            AttackPet();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        timer = 0f;

        // Taking damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
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
