using UnityEngine;
using System.Collections;
using QFSW.QC;

public class WeaponManager : MonoBehaviour
{
    public StatisticsManager statMg;
    // Default
    public GameObject Gun;
    public int damagePerShot = 20;                  
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;

    // Shotgun
    public GameObject NovaGun;
    public int shotgunDamagePerPellet = 20;
    public float rangeNovaGun = 15f;
    public float timeBetweenBulletsNovaGun = 0.5f;

    // Sword
    public GameObject GalaxySword;
	public bool canAttack = true;
	public bool isAttacking = false;
	public float attackCooldown = 0.5f;
    public int damageSword;

    // Weapon management
    int activeWeapon = 0;

    //public GameObject NovaBarrelEnd1;
    //public GameObject NovaBarrelEnd2;
    //public GameObject NovaBarrelEnd3;

    float timer;                                    
    Ray shootRay = new Ray();                                   
    RaycastHit shootHit;                            
    int shootableMask;
    ParticleSystem gunParticles;
    ParticleSystem novaGunParticles;
    LineRenderer gunLine;
    LineRenderer novaGunLine;                 
    AudioSource gunAudio; 
    public AudioSource novaGunAudio;                        
    Light gunLight;                           
    float effectsDisplayTime = 0.2f;
    Ray[] shootRaysNovaGun = new Ray[5];

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
        GalaxySword = GameObject.Find("GalaxySword");
        Gun.SetActive(false);
        NovaGun.SetActive(false);
        GalaxySword.SetActive(false);
        // shootableMaskNovaGun = LayerMask.GetMask("Shootable");
        // novaGunParticles1 = NovaBarrelEnd1.GetComponent<ParticleSystem>();
        // novaGunParticles2 = NovaBarrelEnd2.GetComponent<ParticleSystem>();
        // novaGunParticles3 = NovaBarrelEnd3.GetComponent<ParticleSystem>();
        // novaGunLine1 = NovaBarrelEnd1.GetComponent<LineRenderer>();
        // novaGunLine2 = NovaBarrelEnd2.GetComponent<LineRenderer>();
        // novaGunLine3 = NovaBarrelEnd3.GetComponent<LineRenderer>();
        // novaGunAudio1 = NovaBarrelEnd1.GetComponent<AudioSource>();
        // novaGunAudio2 = NovaBarrelEnd2.GetComponent<AudioSource>();
        // novaGunAudio3 = NovaBarrelEnd3.GetComponent<AudioSource>();
        // novaGunLight1 = NovaBarrelEnd1.GetComponent<Light>();
        // novaGunLight2 = NovaBarrelEnd2.GetComponent<Light>();
        // novaGunLight3 = NovaBarrelEnd3.GetComponent<Light>();
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
        //if (timer >= timeBetweenBulletsNovaGun * effectsDisplayTime)
        //{
        //    DisableEffectsNovaGun();
        //}
    }

    //public void DisableEffectsNovaGun()
    //{
    //    novaGunLine1.enabled = false;
    //    novaGunLine2.enabled = false;
    //    novaGunLine3.enabled = false;
    //    novaGunLight1.enabled = false;
    //    novaGunLight2.enabled = false;
    //    novaGunLight3.enabled = false;
    //}

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        if (activeWeapon == 0 && timer >= timeBetweenBullets && Time.timeScale != 0 || activeWeapon == 1 && timer >= timeBetweenBullets && Time.timeScale != 0)
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
            statMg.RecordShot(false);
        }
    }

    //public void NovaShoot()
    //{
    //    timer = 0f;

    //    novaGunAudio1.Play();
    //    novaGunAudio2.Play();
    //    novaGunAudio3.Play();

    //    novaGunLight1.enabled = true;
    //    novaGunLight2.enabled = true;
    //    novaGunLight3.enabled = true;

    //    novaGunParticles1.Stop();
    //    novaGunParticles2.Stop();
    //    novaGunParticles3.Stop();
    //    novaGunParticles1.Play();
    //    novaGunParticles2.Play();
    //    novaGunParticles3.Play();

    //    novaGunLine1.enabled = true;
    //    novaGunLine2.enabled = true;
    //    novaGunLine3.enabled = true;

    //    for (int i = 0; i < 3; i++)
    //    {
    //        Vector3 direction = transform.forward;
    //        direction += transform.up * Random.Range(-0.05f, 0.05f);
    //        direction += transform.right * Random.Range(-0.05f, 0.05f);

    //        shootRay.origin = transform.position;
    //        shootRay.direction = direction;

    //        if (Physics.Raycast(shootRay, out shootHit, rangeNovaGun, shootableMaskNovaGun))
    //        {
    //            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

    //            if (enemyHealth != null)
    //            {
    //                float distance = Vector3.Distance(transform.position, shootHit.point);
    //                int damage = Mathf.RoundToInt(shotgunDamagePerPellet * (1 - distance / rangeNovaGun));
    //                enemyHealth.TakeDamage(damage, shootHit.point);
    //            }

    //            if (i == 0)
    //            {
    //                novaGunLine1.SetPosition(0, transform.position);
    //                novaGunLine1.SetPosition(1, shootHit.point);
    //            }
    //            else if (i == 1)
    //            {
    //                novaGunLine2.SetPosition(0, transform.position);
    //                novaGunLine2.SetPosition(1, shootHit.point);
    //            }
    //            else if (i == 2)
    //            {
    //                novaGunLine3.SetPosition(0, transform.position);
    //                novaGunLine3.SetPosition(1, shootHit.point);
    //            }
    //        }
    //        else
    //        {
    //            if (i == 0)
    //            {
    //                novaGunLine1.SetPosition(0, transform.position);
    //                novaGunLine1.SetPosition(1, shootRay.origin + shootRay.direction * rangeNovaGun);
    //            }
    //            else if (i == 1)
    //            {
    //                novaGunLine2.SetPosition(0, transform.position);
    //                novaGunLine2.SetPosition(1, shootRay.origin + shootRay.direction * rangeNovaGun);
    //            }
    //            else if (i == 2)
    //            {
    //                novaGunLine3.SetPosition(0, transform.position);
    //                novaGunLine3.SetPosition(1, shootRay.origin + shootRay.direction * rangeNovaGun);
    //            }
    //        }
    //    }
    //}

    public void NovaShoot()
    {
        timer = 0f;

        novaGunAudio.Play();

        //Play gun particle
        gunParticles.Stop();
        gunParticles.Play();

        //enable Line renderer
        gunLine.enabled = true;
        gunLine.positionCount = 10;

        //set shooting rays
        for (int i = 0; i < shootRaysNovaGun.Length; i++)
        {
            gunLine.SetPosition(2 * i, transform.position);
            shootRaysNovaGun[i].origin = transform.position;
            shootRaysNovaGun[i].direction = Quaternion.Euler(0, (-10 + i * 5), 0) * transform.forward;
            //Lakukan raycast jika mendeteksi id nemy hit apapun
            if (Physics.Raycast(shootRaysNovaGun[i], out shootHit, range, shootableMask))
            {
                //Lakukan raycast hit hace component Enemyhealth
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    //Lakukan Take Damage
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }
                statMg.RecordShot(enemyHealth);
                //Set line end position ke hit position
                gunLine.SetPosition(2 * i + 1, shootHit.point);
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

        int maxDamageDefaultGun = Mathf.RoundToInt(2.5f * damagePerShot);
        int maxDamageNovaGun = Mathf.RoundToInt(2.5f * shotgunDamagePerPellet);
        int maxDamageSword = Mathf.RoundToInt(2.5f * damageSword);

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