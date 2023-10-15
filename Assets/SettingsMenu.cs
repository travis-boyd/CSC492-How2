using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void VolumeButton ()
    {
    }

    public void ResolutionButton ()
    {
    }

    public void LanguageButton ()
    {
    }

    public void BackButton()
    {
	// TODO this should return the previous scene, which could be the Menu or the Game
	// Now it always returns to the menu
	SceneManager.LoadScene("Menu");
    }
}
