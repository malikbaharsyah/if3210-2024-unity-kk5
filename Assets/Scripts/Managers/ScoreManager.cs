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
        score = 0;
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
}
