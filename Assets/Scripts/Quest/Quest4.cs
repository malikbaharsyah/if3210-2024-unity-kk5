using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest4 : Quest
{
    public int targetShoot;

    // Start is called before the first frame update
    void Start()
    {
        rewardAmount = 100 * difficulty;
        targetShoot = 20 * difficulty;
        title.text = "Shot Enemies " + targetShoot + " times";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        if (statMg.GetShotAccuracy() >= targetShoot)
        {
            CompleteQuest();
        }
        progress.text = "(" + Mathf.RoundToInt(statMg.GetShotAccuracy()) + "/" + targetShoot + ")";
    }
}
