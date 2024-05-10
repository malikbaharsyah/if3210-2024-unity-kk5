using UnityEngine;
using System;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager Instance { get; private set; }

    public string username;
    private int totalShots;
    private int shotAccuracy;
    private int hurt;
    private float distanceTraveled;
    private float playTime;
    private int enemiesKilled;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        playTime += Time.deltaTime;
    }

    public void RecordShot(bool hit)
    {
        totalShots++;
        if (hit)
        {
            shotAccuracy++;
        }
    }

    public void RecordDistance(float distance)
    {
        distanceTraveled += distance;
    }

    public void RecordEnemyKilled()
    {
        enemiesKilled++;
    }

    public void RecordHurt()
    {
        hurt++;
    }

    public int GetTotalShots()
    {
        return totalShots;
    }

    public int GetShotAccuracy()
    {
        return shotAccuracy;
    }

    public string GetPlayTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(playTime);
        return time.ToString(@"hh\:mm\:ss");
    }

    public float GetDistanceTraveled()
    {
        return distanceTraveled;
    }

    public int GetTotalEnemiesKilled()
    {
        return enemiesKilled;
    }

    public int GetTotalHurts()
    {
        return hurt;
    }

    public static string GetPrefKey(string username, string key)
    {
        return $"{username}_{key}";
    }

    public void SavePlayerStats()
    {
        username = PlayerPrefs.GetString("playerName");
        PlayerPrefs.SetInt(GetPrefKey(username, "TotalShots"), totalShots);
        PlayerPrefs.SetInt(GetPrefKey(username, "Accuracy"), shotAccuracy);
        PlayerPrefs.SetFloat(GetPrefKey(username, "DistanceTraveled"), distanceTraveled);
        PlayerPrefs.SetInt(GetPrefKey(username, "EnemiesKilled"), enemiesKilled);
        PlayerPrefs.SetFloat(GetPrefKey(username, "PlayTime"), playTime);
        PlayerPrefs.Save();
    }

    public void LoadPlayerStats()
    {
        username = PlayerPrefs.GetString("playerName");
        totalShots = PlayerPrefs.GetInt(GetPrefKey(username, "TotalShots"), 0);
        shotAccuracy = PlayerPrefs.GetInt(GetPrefKey(username, "Accuracy"), 0);
        distanceTraveled = PlayerPrefs.GetFloat(GetPrefKey(username, "DistanceTraveled"), 0f);
        enemiesKilled = PlayerPrefs.GetInt(GetPrefKey(username, "EnemiesKilled"), 0);
        playTime = PlayerPrefs.GetFloat(GetPrefKey(username, "PlayTime"), 0f);
    }

}
