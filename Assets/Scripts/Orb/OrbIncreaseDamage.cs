using UnityEngine;
using System.Collections;

public class OrbIncreaseDamage : Orb
{   
    public WeaponManager weaponManager;
    protected override void CollectOrb(GameObject player)
    {
        Debug.Log("Masuk OrbIncreaseDamage");
        weaponManager = player.GetComponentInChildren<WeaponManager>();
        weaponManager.IncreaseDamageByOrb(0.1f);
    }
}
