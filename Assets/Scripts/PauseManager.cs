using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public Canvas pauseScreenCanvas;
    public Button ResumeButton, MainMenuButton, QuitButton;


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.P)))
        {
            // Debug.Log("Paused pressed");
            TogglePause();
        }
    }


    /*
    GUI Buttons
    */

    public void PressResumeButton()
    {
        // Debug.Log("Resume button pressed");
        TogglePause();

    }

    public void PressMainMenuButton()
    {
        //Debug.Log("MainMenu button pressed");
        SceneManager.LoadScene("Menu");
        // code to open main menu
    }

    public void PressQuitButton()
    {
        // Debug.Log("Quit button pressed");
        Application.Quit();
        // code to quit
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused) PauseGame();
        else ResumeGame();
    }

    void PauseGame()
    {
        // Set time scale to 0 to pause the game
        Time.timeScale = 0f;

        // Overlay the pause screen canvas
        if (pauseScreenCanvas != null)
        {
            // Debug.Log("Pause Canvas activated");
            pauseScreenCanvas.gameObject.SetActive(true);
        }
        else if (pauseScreenCanvas = null)
        {
            Debug.LogError("pauseScreenCanvas null 55");
        }
    }

    void ResumeGame()
    {
        // Set time scale back to 1 to resume the game
        Time.timeScale = 1f;

        // Remove overlay of pause screen canvas
        if (pauseScreenCanvas != null)
        {
            pauseScreenCanvas.gameObject.SetActive(false);
        }
        else if (pauseScreenCanvas = null)
        {
            Debug.LogError("pauseScreenCanvas null 71");
        }
    }
}
