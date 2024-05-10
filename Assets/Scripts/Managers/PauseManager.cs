using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Assuming Escape key toggles pause
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0; // Pauses the game
            // Optionally display a pause menu
            ShowPauseMenu(true);
        }
        else
        {
            Time.timeScale = 1; // Resumes the game
            // Optionally hide the pause menu
            ShowPauseMenu(false);
        }
    }

    void ShowPauseMenu(bool show)
    {
        // Assuming you have a pause menu GameObject
        // Enable or disable the pause menu UI
        // Example: pauseMenuGameObject.SetActive(show);
    }
}
