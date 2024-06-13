using UnityEngine;
using System.Collections;

public class OrbIncreaseSpeed : Orb
{   
    public PlayerMovement playerMovement;
    protected override void CollectOrb(GameObject player)
    {
        Debug.Log("Masuk OrbIncreaseSpeed");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.IncreaseSpeedByOrb();
    }
}
