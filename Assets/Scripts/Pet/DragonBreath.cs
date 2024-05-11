using UnityEngine;

public class DragonBreath : MonoBehaviour
{
    public int damageAmount = 20;
    public float damageInterval = 0.5f; 

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
                BaseEnemyHealth enemyHealth = other.GetComponent<BaseEnemyHealth>();
                if (enemyHealth != null)
                {
                    Vector3 hitPoint = other.transform.position;
                    enemyHealth.TakeDamage(damageAmount, hitPoint);
                }
            }

            lastDamageTime = Time.time;
        }
    }
}