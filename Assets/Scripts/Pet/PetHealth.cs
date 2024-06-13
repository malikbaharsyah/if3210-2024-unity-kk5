using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
using System.Diagnostics;

public class PetHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;

    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    bool canTakeDamage = true;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        StartSinking();
    }

    public void StartSinking() {
        // GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }

    public void TakeDamage(int amount) 
    {
        if(!canTakeDamage)
        {
            return;
        }

        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void SetFullHealthPet()
    {
        canTakeDamage = !canTakeDamage;
    }

    [Command("fhpet")]
    private void FullHealthPet()
    {
        SetFullHealthPet();
        UnityEngine.Debug.Log("Cheat Full Health Pet activated");
    }

    [Command("killpet")]
    private void KillPet()
    {
        if (isDead) return;
        Death();
        UnityEngine.Debug.Log("Cheat Kill Pet Activated");
    }
}
