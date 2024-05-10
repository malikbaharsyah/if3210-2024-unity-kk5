using UnityEngine;
using System.Collections;
using QFSW.QC;

public class WeaponManager : MonoBehaviour
{
    public StatisticsManager statMg;
    // Default
    public GameObject Gun;
    public int damagePerShot = 20; 
    int startingDamagePerShot = 20;                 
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;

    // Shotgun
    public GameObject NovaGun;
    public int shotgunDamagePerPellet = 25;
    int startingShotgunDamagePerPellet = 25;
    public float rangeNovaGun = 10f;
    public float timeBetweenBulletsNovaGun = 0.15f;

    // Sword
    public GameObject GalaxySword;
	public bool canAttack = true;
	public bool isAttacking = false;
	public float attackCooldown = 0.5f;
    public int damageSword;
    int startingDamageSword = 25;

    // Weapon management
    int activeWeapon = 0;

    float timer;                                    
    Ray shootRay = new Ray();   
    Ray[] shootRaysNovaGun = new Ray[3];                                
    RaycastHit shootHit;                            
    int shootableMask;
    ParticleSystem gunParticles;
    ParticleSystem novaGunParticles;
    LineRenderer gunLine;
    LineRenderer novaGunLine;                 
    AudioSource gunAudio; 
    public AudioSource novaGunAudio;                        
    Light gunLight;  
    Light novaGunLight;                         
    float effectsDisplayTime = 0.2f;

    void Awake()
    {
        statMg = FindObjectOfType<StatisticsManager>();
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        Gun = GameObject.Find("Gun");
        NovaGun = GameObject.Find("Nova");
        novaGunParticles = NovaGun.GetComponentInChildren<ParticleSystem>();
        novaGunLine = NovaGun.GetComponentInChildren<LineRenderer>();
        novaGunAudio = NovaGun.GetComponentInChildren<AudioSource>();
        novaGunLight = NovaGun.GetComponentInChildren<Light>();
        GalaxySword = GameObject.Find("GalaxySword");
        Gun.SetActive(false);
        NovaGun.SetActive(false);
        GalaxySword.SetActive(false);
        damageSword = GalaxySword.GetComponentInChildren<SwordCollider>().damage;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeWeapon = 2;
        }

        if (activeWeapon == 0)
        {
            Gun.SetActive(true);
            GalaxySword.SetActive(false);
            NovaGun.SetActive(false);
        }
        else if (activeWeapon == 1)
        {
            Gun.SetActive(false);
            NovaGun.SetActive(true);
            GalaxySword.SetActive(false);
        }
        else if (activeWeapon == 2)
        {
            Gun.SetActive(false);
            NovaGun.SetActive(false);
            GalaxySword.SetActive(true);
        }

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
        if (timer >= timeBetweenBulletsNovaGun * effectsDisplayTime)
        {
            DisableEffectsNovaGun();
        }
    }

    public void DisableEffectsNovaGun()
    {
        novaGunLine.enabled = false;
        novaGunLight.enabled = false;
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        if (activeWeapon == 0 && timer >= timeBetweenBullets && Time.timeScale != 0 || activeWeapon == 1 && timer >= timeBetweenBulletsNovaGun && Time.timeScale != 0)
        {
            if (activeWeapon == 0)
            {
                DefaultGun();
            }
            else if (activeWeapon == 1)
            {
                NovaShoot();
            }
        }
        else if (activeWeapon == 2 && canAttack && Time.timeScale != 0)
        {
            Sword();
        }
    }

    public void DefaultGun()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            statMg.RecordShot(enemyHealth);
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    public void NovaShoot()
    {
        timer = 0f;
        novaGunAudio.Play();
        novaGunLight.enabled = true;
        novaGunParticles.Stop();
        novaGunParticles.Play();
        novaGunLine.enabled = true;
        novaGunLine.positionCount = 6;

        for (int i = 0; i < shootRaysNovaGun.Length; i++)
        {
            novaGunLine.SetPosition(2 * i, transform.position);
            shootRaysNovaGun[i].origin = transform.position;
            shootRaysNovaGun[i].direction = Quaternion.Euler(0, (-5 + i * 5), 0) * transform.forward;
            if (Physics.Raycast(shootRaysNovaGun[i], out shootHit, rangeNovaGun, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(shotgunDamagePerPellet, shootHit.point);
                }
                statMg.RecordShot(enemyHealth);
                //Set line end position ke hit position
                gunLine.SetPosition(2 * i + 1, shootHit.point);
                novaGunLine.SetPosition(2 * i + 1, shootHit.point);
            }
            else
            {
                //set line end position ke range freom barrel
                gunLine.SetPosition(2 * i + 1, shootRaysNovaGun[i].origin + shootRaysNovaGun[i].direction * range);
                statMg.RecordShot(false);
            }
        }
    }

    public void Sword()
    {
        GalaxySword.GetComponent<PlayerSwording>().SwordAttack();
    }

    public void IncreaseDamageByOrb(float percentage)
    {   
        Debug.Log("Damage increased terpanggil");

        int maxDamageDefaultGun = Mathf.RoundToInt(2.5f * startingDamagePerShot);
        int maxDamageNovaGun = Mathf.RoundToInt(2.5f * startingShotgunDamagePerPellet);
        int maxDamageSword = Mathf.RoundToInt(2.5f * startingDamageSword);

        damagePerShot = Mathf.Min(maxDamageDefaultGun, damagePerShot + Mathf.RoundToInt(percentage * damagePerShot));
        shotgunDamagePerPellet = Mathf.Min(maxDamageNovaGun, shotgunDamagePerPellet + Mathf.RoundToInt(percentage * shotgunDamagePerPellet));
        damageSword = Mathf.Min(maxDamageSword, damageSword + Mathf.RoundToInt(percentage * damageSword));

        Debug.Log("Damage Default Gun increased to " + damagePerShot);
        Debug.Log("Damage Shotgun increased to " + shotgunDamagePerPellet);
        Debug.Log("Damage Sword increased to " + damageSword);
    }

    public void SetOneHitKill()
    {
        EnemyHealth enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
        damagePerShot = enemyHealth.startingHealth;
        shotgunDamagePerPellet = enemyHealth.startingHealth;
        damageSword = enemyHealth.startingHealth;
    }

    [Command("1kill")]
    private void OneHitKill()
    {
        SetOneHitKill();
        Debug.Log("Cheat One Kill Activated");
    }
}