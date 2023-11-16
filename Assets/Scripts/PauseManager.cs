using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.P)))
        {
            Debug.Log("Paused pressed");
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused) PauseGame();
        else ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Set time scale to 0 to pause the game
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Set time scale back to 1 to resume the game
    }
}
