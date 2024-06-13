using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoss : Quest
{
    public GameObject raja;

    BaseEnemyHealth rajaHealth;

    // Start is called before the first frame update
    void Start()
    {
        rajaHealth = raja.GetComponent<RajaHealth>();

        rewardAmount = 1000 * difficulty;
        title.text = "Kill RAJA!!!";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        if ( rajaHealth.currentHealth <= 0)
        {
            progress.text = "( Raja HP = 0 )";
            CompleteQuest();
        }

        progress.text = "( Raja HP = " + Mathf.RoundToInt(rajaHealth.currentHealth) + ")";

    }
}
