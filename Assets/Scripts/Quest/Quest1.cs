using UnityEngine;
using TMPro;

public class Quest1 : Quest
{
    public float finishTime = 60f;
    private float timer;

    void Start()
    {
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
