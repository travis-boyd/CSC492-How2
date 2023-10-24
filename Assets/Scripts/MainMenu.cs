using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
    }

    public void OpenSettings ()
    {
	// TODO some code to let the Settings "Back" button return correctly
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
