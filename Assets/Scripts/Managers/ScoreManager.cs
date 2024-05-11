using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using QFSW.QC;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public GlobalStatistics globalStat;


    TextMeshProUGUI text;


    void Awake ()
    {
        globalStat = FindObjectOfType<GlobalStatistics>();
        text = GetComponent<TextMeshProUGUI>();
        LoadScore();
    }


    void Update ()
    {
        text.text = score.ToString();
        globalStat.SetCoin(score);
    }

    public int getScore()
    {
        return score;
    }

    public void setScore(int inputScore)
    {
        score = inputScore;
    }

    public void SaveScore()
    {
        string username = PlayerPrefs.GetString("playerName", "Player");
        PlayerPrefs.SetInt(username+"_Coin", score);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        string username = PlayerPrefs.GetString("playerName", "Player");
        score = PlayerPrefs.GetInt(username+"_Coin", 0);
    }

    public void ResetScore()
    {
        string username = PlayerPrefs.GetString("playerName", "Player");
        PlayerPrefs.SetInt(username + "_Coin", 0);
        score = 0;
    }

    [Command("motherlode")]
    private void Motherlode()
    {
        setScore(int.MaxValue);
        UnityEngine.Debug.Log("Cheat Infinity Money Activated");
    }
}
