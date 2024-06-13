using UnityEngine;
using TMPro;

public class Quest1 : Quest
{
    private float finishTime;
    private float timer;

    void Start()
    {
        rewardAmount = 100 * difficulty;
        finishTime = 60f * difficulty;
        title.text = "Survive for " + finishTime + "s";
    }

    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if (timer >= finishTime)
        {
            CompleteQuest();
        }
        progress.text = "(" + Mathf.RoundToInt(timer) + "/" + finishTime + ")";
    }
}
