using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class Quest3 : Quest
{
    public float targetDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        rewardAmount = 100 * difficulty;
        targetDistance = 100f * difficulty;
        title.text = "Walk around " + targetDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        if (statMg.GetDistanceTraveled() >= targetDistance)
        {
            CompleteQuest();
        }
        progress.text = "(" + Mathf.RoundToInt(statMg.GetDistanceTraveled()) + "/" + targetDistance + ")";
    }
}
