using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class DragonMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float followDistance = 20f;
    public float followDistanceLimit = 10f;
    private PetHealth petHealth;
    private Animator anim;
    private Rigidbody petRigidBody;
    private NavMeshAgent agent;
    private Transform closestEnemy;
    private GameObject dragonFlames;

    void Awake()
    {
        dragonFlames = transform.Find("FlamesEffect").gameObject;
        anim = GetComponent<Animator>();
        petRigidBody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        petHealth = GetComponent<PetHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dragonFlames.SetActive(false);
    }

    void Update()
    {
        if (petHealth.currentHealth > 0)
        {
            FindClosestEnemy();
            if (closestEnemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, closestEnemy.position);
                Vector3 direction = (closestEnemy.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                if (distanceToEnemy > followDistance)
                {
                    Vector3 offset = direction.normalized * (distanceToEnemy - followDistanceLimit);
                    Vector3 targetPosition = closestEnemy.position - offset;
                    agent.SetDestination(targetPosition);
                    anim.SetBool("IsFlying", true);
                    dragonFlames.SetActive(true);

                } else
                {
                    agent.ResetPath();
                }
            }
        } else
        {
            agent.enabled = false;
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

    void MoveAway()
    {
        Vector3 direction = transform.position - closestEnemy.position;
        direction.Normalize();
        Vector3 destination = transform.position + direction * followDistanceLimit;
        agent.SetDestination(destination);
    }
}
