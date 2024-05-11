using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using QFSW.QC;

public class Quest : MonoBehaviour
{
    public bool isActive = true;
    public string nextScene;
    public LocalStatistics statMg;
    public TextMeshProUGUI title;
    public TextMeshProUGUI progress;
    public GameObject nextButton;
    public GameObject saveButton;
    public int rewardAmount;
    private Button nextBtn;
    private Button saveBtn;

    void Awake()
    {
        statMg = FindObjectOfType<LocalStatistics>();
        title = transform.Find("MissionTitle").GetComponent<TextMeshProUGUI>();
        progress = transform.Find("Progress").GetComponent<TextMeshProUGUI>();
        nextButton = transform.Find("NextButton").gameObject;
        nextBtn = nextButton.GetComponent<Button>();
        saveButton = transform.Find("SaveButton").gameObject;
        saveBtn = saveButton.GetComponent<Button>();
        if (nextBtn != null )
        {
            nextBtn.onClick.AddListener(NextScene);
        }
        if (saveBtn != null )
        {
            saveBtn.onClick.AddListener(SaveScene);
        }
    }

    public void CompleteQuest()
    {
        isActive = false;
        DestroyAllEnemies();
        StopSpawningEnemies();
        nextButton.SetActive(true);
        saveButton.SetActive(true);
        ScoreManager.score += rewardAmount;
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

    public void SaveScene()
    {
        string username = PlayerPrefs.GetString("playerName");
        PlayerPrefs.SetString(username + "_scene", nextScene);
        SceneManager.LoadScene("Save");
    }

    [Command("skip")]
    private void SkipLevel()
    {
        SceneManager.LoadScene(nextScene);
        UnityEngine.Debug.Log("Cheat Skip Level Activated");
    }
}
