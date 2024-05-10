using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    public bool isActive = true;
    public string nextScene;
    public LocalStatistics statMg;
    public TextMeshProUGUI title;
    public TextMeshProUGUI progress;
    public GameObject nextButton;
    public int rewardAmount;
    private Button nextBtn;

    void Awake()
    {
        statMg = FindObjectOfType<LocalStatistics>();
        title = transform.Find("MissionTitle").GetComponent<TextMeshProUGUI>();
        progress = transform.Find("Progress").GetComponent<TextMeshProUGUI>();
        nextButton = transform.Find("NextButton").gameObject;
        nextBtn = nextButton.GetComponent<Button>();
        if (nextBtn != null )
        {
            nextBtn.onClick.AddListener(NextScene);
        }
    }

    public void CompleteQuest()
    {
        isActive = false;
        DestroyAllEnemies();
        StopSpawningEnemies();
        nextButton.SetActive(true);
    }

    public void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void StopSpawningEnemies()
    {
        EnemyManager[] spawners = FindObjectsOfType<EnemyManager>();
        foreach (EnemyManager spawner in spawners)
        {
            if (spawner != null)
            {
                spawner.StopSpawning();
            }
        }

    }

    public void NextScene()
    {
        if (!isActive)
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.SaveScore();
            SceneManager.LoadScene(nextScene);
        }
    }
}
