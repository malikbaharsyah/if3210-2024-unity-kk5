using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    public Text saveGame1;
    public Text saveGame2;
    public Text saveGame3;
    public Text playTime1;
    public Text playTime2;
    public Text playTime3;
    public GlobalStatistics globalStats;
    //public ScoreManager scoreMg;
    void Start()
    {
        //scoreMg = FindObjectOfType<ScoreManager>();
        globalStats = FindObjectOfType<GlobalStatistics>();
        UpdateSaveSlots();
    }

    public void SaveGameButton1()
    {
        string username = PlayerPrefs.GetString("playerName");
        //scoreMg.SaveScore();
        PlayerPrefs.SetString("saveGame1", username);
        string playTime = globalStats.GetPlayTime();
        PlayerPrefs.SetString("playTime1", playTime);
        saveGame1.text = username;
        playTime1.text = playTime;
        string scene = PlayerPrefs.GetString(username + "_scene", "Cutscene1");
        SceneManager.LoadScene(scene);
    }

    public void SaveGameButton2()
    {
        string username = PlayerPrefs.GetString("playerName");
        //scoreMg.SaveScore();
        PlayerPrefs.SetString("saveGame2", username);
        string playTime = globalStats.GetPlayTime();
        PlayerPrefs.SetString("playTime2", playTime);
        saveGame2.text = username;
        playTime2.text = playTime;
        string scene = PlayerPrefs.GetString(username + "_scene", "Cutscene1");
        SceneManager.LoadScene(scene);
    }

    public void SaveGameButton3()
    {
        string username = PlayerPrefs.GetString("playerName");
        //scoreMg.SaveScore();
        PlayerPrefs.SetString("saveGame3", username);
        string playTime = globalStats.GetPlayTime();
        PlayerPrefs.SetString("playTime3", playTime);
        saveGame3.text = username;
        playTime3.text = playTime;
        string scene = PlayerPrefs.GetString(username + "_scene", "Cutscene1");
        SceneManager.LoadScene(scene);
    }

    public void UpdateSaveSlots()
    {
        if (!PlayerPrefs.HasKey("saveGame1"))
        {
            PlayerPrefs.SetString("saveGame1", "Empty");
        }
        if (!PlayerPrefs.HasKey("saveGame2"))
        {
            PlayerPrefs.SetString("saveGame2", "Empty");
        }
        if (!PlayerPrefs.HasKey("saveGame3"))
        {
            PlayerPrefs.SetString("saveGame3", "Empty");
        }
        if (!PlayerPrefs.HasKey("playTime1"))
        {
            PlayerPrefs.SetString("playTime1", "00:00:00");
        }
        if (!PlayerPrefs.HasKey("playTime2"))
        {
            PlayerPrefs.SetString("playTime2", "00:00:00");
        }
        if (!PlayerPrefs.HasKey("playTime3"))
        {
            PlayerPrefs.SetString("playTime3", "00:00:00");
        }
        saveGame1.text = PlayerPrefs.GetString("saveGame1");
        saveGame2.text = PlayerPrefs.GetString("saveGame2");
        saveGame3.text = PlayerPrefs.GetString("saveGame3");

        playTime1.text = PlayerPrefs.GetString("playTime1");
        playTime2.text = PlayerPrefs.GetString("playTime2");
        playTime3.text = PlayerPrefs.GetString("playTime3");
    }
}
