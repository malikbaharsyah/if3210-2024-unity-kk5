using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour {

    Animator anim;
    //public ScoreManager scoreMg;
    public int newGameScene;
    // public int quickSaveSlotID;

    [Header("Options Panel")]
    public GameObject MainOptionsPanel;
    public GameObject StartGameOptionsPanel;
    public GameObject GamePanel;
    public GameObject ControlsPanel;
    public GameObject GfxPanel;
    public GameObject LoadGamePanel;

    public Slider musicVol, sfxVol;
    public AudioMixer masterMixer;

    public TMP_InputField playerNameInput;
    public TMP_Dropdown difficultyDropdown;

    public GlobalStatistics globalStats;
    public Text playerVal;
    public Text totalShootVal;
    public Text shotAccVal;
    public Text enemyKillVal;
    public Text distTravVal;
    public Text hurtVal;
    public Text playTimeVal;

    public Text saveGame1;
    public Text saveGame2;
    public Text saveGame3;
    public Text playTime1;
    public Text playTime2;
    public Text playTime3;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        //scoreMg = FindObjectOfType<ScoreManager>();
        //new key
        // PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);
        if (!PlayerPrefs.HasKey("playerName")) {
            PlayerPrefs.SetString("playerName", "Player");
        }
        if (!PlayerPrefs.HasKey("difficulty")) {
            PlayerPrefs.SetInt("difficulty", 0);
        }
        globalStats = FindObjectOfType<GlobalStatistics>();
        globalStats.LoadPlayerStats();
        UpdateLoadSlots();
    }

    public void openOptions()
    {
        //enable respective panel
        MainOptionsPanel.SetActive(true);
        StartGameOptionsPanel.SetActive(false);

        //play anim for opening main options panel
        anim.Play("buttonTweenAnims_on");

        //play click sfx
        playClickSound();

        //enable BLUR
        //Camera.main.GetComponent<Animator>().Play("BlurOn");
    }

    public void openStartGameOptions()
    {
        //enable respective panel
        MainOptionsPanel.SetActive(false);
        StartGameOptionsPanel.SetActive(true);

        //play anim for opening main options panel
        anim.Play("buttonTweenAnims_on");

        //play click sfx
        playClickSound();

        //enable BLUR
        //Camera.main.GetComponent<Animator>().Play("BlurOn");
        
    }

    public void openOptions_Game()
    {
        //enable respective panel
        GamePanel.SetActive(true);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(false);

        
        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");
        globalStats.LoadPlayerStats();
        UnityEngine.Debug.Log(globalStats.GetTotalHurts());
        playerVal.text = PlayerPrefs.GetString("playerName");
        totalShootVal.text = globalStats.GetTotalShots().ToString();
        shotAccVal.text = globalStats.GetShotAccuracy().ToString();
        enemyKillVal.text = globalStats.GetTotalEnemiesKilled().ToString();
        distTravVal.text = globalStats.GetDistanceTraveled().ToString();
        hurtVal.text = globalStats.GetTotalHurts().ToString();
        playTimeVal.text = globalStats.GetPlayTime();

        //play click sfx
        playClickSound();

    }
    public void openOptions_Controls()
    {
        //enable respective panel
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(true);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(false);

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }
    public void openOptions_Gfx()
    {
        //enable respective panel
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(true);
        LoadGamePanel.SetActive(false);

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }

    public void openContinue_Load()
    {
        //enable respective panel
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(true);

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }

    public void newGame()
    {
        newGameScene = SceneManager.GetActiveScene().buildIndex + 1;
        string username = PlayerPrefs.GetString("playerName");
        //scoreMg.ResetScore();
        globalStats.ResetStats();
        SceneManager.LoadScene(newGameScene);
    }

    public void back_options()
    {
        //simply play anim for CLOSING main options panel
        anim.Play("buttonTweenAnims_off");

        //disable BLUR
       // Camera.main.GetComponent<Animator>().Play("BlurOff");

        //play click sfx
        playClickSound();
    }

    public void back_options_panels()
    {
        //simply play anim for CLOSING main options panel
        anim.Play("OptTweenAnim_off");
        
        //play click sfx
        playClickSound();

    }

    public void Quit()
    {
        Application.Quit();
    }
    public void playHoverClip()
    {

    }

    void playClickSound() {

    }

    public void SetMusicVolume()
    {
        float value = musicVol.value;
        masterMixer.SetFloat("musicVol", value);
    }

    public void SetSFXVolume()
    {
        float value = sfxVol.value;
        masterMixer.SetFloat("sfxVol", value);
    }

    public void SetPlayerName()
    {
        string playerName = playerNameInput.text;
        PlayerPrefs.SetString("playerName", playerName);
        globalStats.LoadPlayerStats();
        Debug.Log("Player name set to: " + playerName);
    }

    public void SetDifficulty()
    {
        int difficulty = difficultyDropdown.value;
        PlayerPrefs.SetInt("difficulty", difficulty);
        Debug.Log("Difficulty set to: " + difficulty);
        // TODO - set difficulty level 
    }

    public void LoadGame1()
    {
        string username = PlayerPrefs.GetString("saveGame1");
        PlayerPrefs.SetString("playerName", username);
        globalStats.LoadPlayerStats();
        string scene = PlayerPrefs.GetString(username+"_scene", "Cutscene1");
        //scoreMg.LoadScore();
        SceneManager.LoadScene(scene);
    }
    public void LoadGame2()
    {
        string username = PlayerPrefs.GetString("saveGame2");
        PlayerPrefs.SetString("playerName", username);
        globalStats.LoadPlayerStats();
        string scene = PlayerPrefs.GetString(username + "_scene", "Cutscene1");
        //scoreMg.LoadScore();
        SceneManager.LoadScene(scene);
    }
    public void LoadGame3()
    {
        string username = PlayerPrefs.GetString("saveGame3");
        PlayerPrefs.SetString("playerName", username);
        globalStats.LoadPlayerStats();
        string scene = PlayerPrefs.GetString(username + "_scene", "Cutscene1");
        //scoreMg.LoadScore();
        SceneManager.LoadScene(scene);
    }
    public void UpdateLoadSlots()
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
