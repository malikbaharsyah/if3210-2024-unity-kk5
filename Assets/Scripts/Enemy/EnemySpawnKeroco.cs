using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnKeroco : MonoBehaviour
{
    public float timeBetweenSpawns = 15f;
    public GameObject keroco;
    PlayerHealth playerHealth;
    Animator animator;
    float timer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenSpawns && playerHealth.currentHealth > 0)
        {
            animator.SetBool("isSpawningKeroco", true);
        }
    }

    public void ResetTimer()
    {
        timer = 0f;
        animator.SetBool("isSpawningKeroco", false);
    }

    public void SpawnKeroco()
    {
        Vector3 spawnPosition = animator.transform.position + new Vector3(5, 0, 5);
        Instantiate(keroco, animator.transform.position, animator.transform.rotation);
    }
}
