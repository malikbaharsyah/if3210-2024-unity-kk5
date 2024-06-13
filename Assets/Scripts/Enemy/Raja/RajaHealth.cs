using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RajaHealth : BaseEnemyHealth
{
    protected override void Death()
    {
        statMg.RecordEnemyKilled();
        locStatMg.RecordEnemyKilled();
        isDead = true;

        capsuleCollider.isTrigger = true;

        int randomValue = Random.Range(0, 3);

        switch (randomValue)
        {
            case 0:
                anim.SetTrigger("Dead0");
                break;
            case 1:
                anim.SetTrigger("Dead1");
                break;
            case 2:
                anim.SetTrigger("Dead2");
                break;
        }

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        SpawnOrb();
    }
}
