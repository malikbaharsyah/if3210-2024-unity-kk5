using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
    // Default
    public GameObject Gun;
    public int damagePerShot = 20;                  
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;

    // Shotgun
    public GameObject NovaGun;
    public int shotgunDamagePerPellet = 20;
    public int shotgunPelletCount = 10;
    public float shotgunSpreadAngle = 20f;

    // Sword
    public GameObject GalaxySword;
	public bool canAttack = true;
	public bool isAttacking = false;
	public float attackCooldown = 0.5f;   

    // Weapon management
    int activeWeapon = 0; 

    float timer;                                    
    Ray shootRay = new Ray();                                   
    RaycastHit shootHit;                            
    int shootableMask;                             
    ParticleSystem gunParticles;                    
    LineRenderer gunLine;                           
    AudioSource gunAudio;                           
    Light gunLight;                                 
    float effectsDisplayTime = 0.2f;                

    void Awake()
    {
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
        GalaxySword.SetActive(true);
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
    }

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
        gunAudio.Play();
        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        for (int i = 0; i < shotgunPelletCount; i++)
        {
            Vector3 spreadVector = Random.insideUnitCircle * shotgunSpreadAngle;
            Vector3 direction = transform.forward + spreadVector.normalized;

            shootRay.origin = transform.position;
            shootRay.direction = direction;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(shotgunDamagePerPellet, shootHit.point);
                }

                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
    }

    public void Sword()
    {
        GalaxySword.GetComponent<PlayerSwording>().SwordAttack();
    }

    
}