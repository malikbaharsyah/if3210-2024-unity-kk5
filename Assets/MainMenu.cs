using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public Text startText;
    public Text exitText;

    public Color normalColor;
    public Color highlightColor;
    public Color clickColor;

    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void OnStartButtonEnter()
    {
        startText.color = highlightColor;
    }

    public void OnStartButtonClick()
    {
        startText.color = clickColor;
    }

    public void OnExitButtonEnter()
    {
        exitText.color = highlightColor;
    }

    public void OnExitButtonClick()
    {
        exitText.color = clickColor;
    }

    public void OnButtonExit()
    {
        startText.color = normalColor;
        exitText.color = normalColor;
    }
}
