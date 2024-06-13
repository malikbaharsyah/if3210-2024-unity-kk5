using UnityEngine;

public class BaseEnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public GlobalStatistics statMg;
    public LocalStatistics locStatMg;

    public GameObject[] orbPrefabs;


    protected Animator anim;
    protected AudioSource enemyAudio;
    protected ParticleSystem hitParticles;
    protected CapsuleCollider capsuleCollider;
    protected bool isDead;
    protected bool isSinking;

    void Awake()
    {
        // Mendapatkan reference component
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        statMg = FindObjectOfType<GlobalStatistics>();
        locStatMg = FindObjectOfType<LocalStatistics>();

        // Set current health
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (isSinking)
        {
            // memindahkan object ke bawah
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamageSword(int amount)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    protected virtual void Death()
    {
        statMg.RecordEnemyKilled();
        locStatMg.RecordEnemyKilled();

        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        SpawnOrb();
    }


    public void StartSinking()
    {
        // Disable NavMesh component
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // Set Rigidbody ke kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }

    public void SpawnOrb()
    {
        int randomIndex = Random.Range(0, orbPrefabs.Length);
        // postion collider y 0.35 agar diatas tanah
        GameObject orb = Instantiate(orbPrefabs[randomIndex], new Vector3(transform.position.x, 0.35f, transform.position.z), Quaternion.identity);
        orb.transform.SetParent(transform.parent);
    }
}