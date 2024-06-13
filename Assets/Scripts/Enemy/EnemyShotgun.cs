using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShotgun : EnemyWeapon
{
    // Shotgun
    //public GameObject NovaGun;
    public int shotgunDamagePerPellet = 25;
    public float rangeNovaGun = 10f;
    public float timeBetweenBulletsNovaGun = 0.15f;

    float timer;
    Ray[] shootRaysNovaGun = new Ray[3];
    RaycastHit shootHit;
    ParticleSystem novaGunParticles;
    LineRenderer novaGunLine;
    AudioSource novaGunAudio;
    Light novaGunLight;
    float effectsDisplayTime = 0.2f;

    override protected void Awake()
    {
        base.Awake();

        novaGunParticles = GetComponent<ParticleSystem>();
        novaGunLine = GetComponent<LineRenderer>();
        novaGunAudio = GetComponent<AudioSource>();
        novaGunLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;

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

    public void Shoot()
    {
        if (timer >= timeBetweenBulletsNovaGun && Time.timeScale != 0 && enemyHealth.currentHealth > 0)
        {
            NovaShoot();
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
            if (
                Physics.Raycast(shootRaysNovaGun[i], out shootHit, rangeNovaGun) &&
                shootHit.collider.gameObject == player &&
                shootHit.collider.isTrigger == false
                )
            {
                if (playerHealth.currentHealth > 0)
                {
                    float distance = Vector3.Distance(transform.position, shootHit.point);
                    int damage = Mathf.RoundToInt(shotgunDamagePerPellet * (1 - distance / rangeNovaGun));

                    playerHealth.TakeDamage(damage);
                }
                novaGunLine.SetPosition(2 * i + 1, shootHit.point);
            }
            else
            {
                novaGunLine.SetPosition(2 * i + 1, shootRaysNovaGun[i].origin + shootRaysNovaGun[i].direction * rangeNovaGun);
            }
        }
    }
}
