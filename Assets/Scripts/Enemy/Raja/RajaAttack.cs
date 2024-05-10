using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RajaAttack : BaseEnemyAttack
{
    public float timeBetweenSpawns = 15f;
    public GameObject keroco;
    Animator animator;
    float timer;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SpawnKeroco()
    {
        Vector3 spawnPosition = animator.transform.position + new Vector3(5, 0, 5);
        Instantiate(keroco, animator.transform.position, Quaternion.identity);
    }

}
