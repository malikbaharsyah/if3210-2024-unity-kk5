using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    //Animator anim;
    
    public GlobalStatistics globalStats;
    public Text playerVal;
    public Text totalShootVal;
    public Text shotAccVal;
    public Text enemyKillVal;
    public Text distTravVal;
    public Text hurtVal;
    public Text playTimeVal;

    void Start () {
        //anim = GetComponent<Animator>();
        //anim.Play("OptTweenAnim_on");
        globalStats = FindObjectOfType<GlobalStatistics>();
        globalStats.LoadPlayerStats();
        playerVal.text = PlayerPrefs.GetString("playerName");
        totalShootVal.text = globalStats.GetTotalShots().ToString();
        shotAccVal.text = globalStats.GetShotAccuracy().ToString();
        enemyKillVal.text = globalStats.GetTotalEnemiesKilled().ToString();
        distTravVal.text = globalStats.GetDistanceTraveled().ToString();
        hurtVal.text = globalStats.GetTotalHurts().ToString();
        playTimeVal.text = globalStats.GetPlayTime();
    }


    public void goToMainMenu()
    {
        playClickSound();
        SceneManager.LoadScene(0);
    }

    public void tryAgain()
    {
        playClickSound();
        SceneManager.LoadScene(1);
    }

    public void playHoverClip()
    {

    }

    void playClickSound() {

    }
}
