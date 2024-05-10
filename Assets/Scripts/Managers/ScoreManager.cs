using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

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
        PlayerPrefs.SetInt("PlayerCoin", score);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        score = PlayerPrefs.GetInt("PlayerCoin", 0);
    }
}
