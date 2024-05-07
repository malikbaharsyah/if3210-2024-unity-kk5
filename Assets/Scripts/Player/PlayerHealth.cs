using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using QFSW.QC;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    WeaponManager weapon;
    bool isDead;
    bool damaged;

    private bool canTakeDamage = true;

    void Awake()
    {
        // Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        weapon = GetComponentInChildren<WeaponManager>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (damaged)
        {
            // Merubah warna gambar menjadi value dari flashColour
            damageImage.color = flashColour;
        }
        else
        {
            // Fade out damage image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    // Fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        if (!canTakeDamage)
        {
            return;
        }
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        weapon.DisableEffects();

        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        weapon.enabled = false;
    }

    public void RestartLevel()
    {
        //meload ulang scene dengan index 0 pada build setting
        SceneManager.LoadScene(0);
    }

    public void RestoreHealthByOrb(float percentage)
    {   
        int beforeHealth = currentHealth;
        float restoredHealth = startingHealth * percentage;
        currentHealth = Mathf.Min(startingHealth, currentHealth + Mathf.RoundToInt(restoredHealth));
        healthSlider.value = currentHealth;
        Debug.Log("Player health restored from " + beforeHealth + " to " + currentHealth);
    }

    public void SetNoDamage()
    {
        canTakeDamage = !canTakeDamage;
    }

    [Command("nodamage")]
    private void NoDamage()
    {
        SetNoDamage();
        Debug.Log("Cheat No Damage Activated");
    }

    [Command("orb")]
    private void Orb()
    {
        RestoreHealthByOrb(0.2f);
        weapon.IncreaseDamageByOrb(1.5f);
        playerMovement.IncreaseSpeedByOrb(0.2f, 15f);
    }
}
