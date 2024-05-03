using UnityEngine;

public class DragonBreath : MonoBehaviour
{
    public int damageAmount = 20;  // The amount of damage each enemy takes when hit by the dragon breath
    public float damageInterval = 0.5f;  // The interval in seconds at which damage is applied

    private float lastDamageTime;

    void Start()
    {
        lastDamageTime = Time.time;
    }

    void OnParticleCollision(GameObject other)
    {
        if (Time.time >= lastDamageTime + damageInterval)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // Approximate hit point as the position of the enemy GameObject
                    Vector3 hitPoint = other.transform.position;
                    enemyHealth.TakeDamage(damageAmount, hitPoint);
                }
            }

            lastDamageTime = Time.time;
        }
    }
}