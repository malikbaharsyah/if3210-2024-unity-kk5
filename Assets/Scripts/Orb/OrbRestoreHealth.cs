using UnityEngine;
using System.Collections;

public class OrbRestoreHealth : Orb
{
    protected override void CollectOrb(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        Debug.Log("Masuk OrbRestoreHealth");
        playerHealth.RestoreHealthByOrb(0.2f);
    }
}
