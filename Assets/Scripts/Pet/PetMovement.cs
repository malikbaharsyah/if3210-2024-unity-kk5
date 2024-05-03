using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class PetMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float followDistance = 10f;
    private Animator anim;
    private Rigidbody petRigidBody;
    private NavMeshAgent agent;
    private Transform closestEnemy;
    private GameObject dragonFlames;

    void Awake()
    {
        // get the dragon flames effect (yellow and blue ones)
        dragonFlames = GameObject.FindGameObjectWithTag("DragonFlames");
        anim = GetComponent<Animator>();
        petRigidBody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dragonFlames.SetActive(false);
    }

    void FixedUpdate()
    {
        FindClosestEnemy();
        if (closestEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, closestEnemy.position);
            if (distanceToEnemy > followDistance)
            {
                agent.SetDestination(closestEnemy.position);
                anim.SetBool("IsFlying", true);
                dragonFlames.SetActive(true);

            }
            else
            {
                agent.SetDestination(transform.position);
                anim.SetBool("IsFlying", false);
                dragonFlames.SetActive(false);
            }
        }
    }

    void FindClosestEnemy()
    {
        float minDist = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(player.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestEnemy = enemy.transform;
            }
        }
    }
}
