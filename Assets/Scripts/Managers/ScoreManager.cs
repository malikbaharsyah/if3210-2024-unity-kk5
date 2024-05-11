using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using QFSW.QC;

public class ScoreManager : MonoBehaviour
{
    public static int score;


    TextMeshProUGUI text;


    void Awake ()
    {
        text = GetComponent<TextMeshProUGUI>();
        LoadScore();
    }


    void Update ()
    {
        text.text = score.ToString();
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
        PlayerPrefs.SetInt(username+"_coin", score);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        string username = PlayerPrefs.GetString("playerName", "Player");
        score = PlayerPrefs.GetInt(username+"_coin", 0);
    }

    [Command("motherlode")]
    private void Motherlode()
    {
        setScore(int.MaxValue);
        UnityEngine.Debug.Log("Cheat Infinity Money Activated");
    }
}
