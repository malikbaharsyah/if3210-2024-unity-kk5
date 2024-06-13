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
    public GlobalStatistics statMg;
    public LocalStatistics locStatMg;


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    WeaponManager weapon;
    bool isDead;
    bool damaged;

    private bool canTakeDamage = true;
    GameObject healEffect;

    void Awake()
    {
        statMg = FindObjectOfType<GlobalStatistics>();
        locStatMg = FindObjectOfType<LocalStatistics>();
        // Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        weapon = GetComponentInChildren<WeaponManager>();
        currentHealth = startingHealth;

        healEffect = transform.Find("HealEffect").gameObject;
        healEffect.SetActive(false);
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
        statMg.RecordHurt();
        locStatMg.RecordHurt();
        UnityEngine.Debug.Log(statMg.GetTotalHurts());
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
        StartCoroutine(ActivateHealEffect());
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
        playerMovement.IncreaseSpeedByOrb();
    }
    public void Heal(int amount)
    {
        int tempHealth = currentHealth + amount;

        currentHealth = Mathf.Min(100, tempHealth);

        healthSlider.value = currentHealth;

        StartCoroutine(ActivateHealEffect());
    }

    IEnumerator ActivateHealEffect()
    {
        healEffect.SetActive(true);

        yield return new WaitForSeconds(1);

        healEffect.SetActive(false);
    }

}
