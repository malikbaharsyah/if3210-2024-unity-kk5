using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class Quest2 : Quest
{
    public int targetKill = 5;

    // Start is called before the first frame update
    void Start()
    {
        title.text = "Kill " + targetKill + "enemies";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        if (statMg.GetTotalEnemiesKilled() >= targetKill)
        {
            CompleteQuest();
        }
        progress.text = "(" + statMg.GetTotalEnemiesKilled() + "/" + targetKill + ")";
    }
}
