using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static int DEFAULT_RES_WIDTH = 1920;
    private static int DEFAULT_RES_HEIGHT = 1080;

    private void Start()
    {
        // Load any settings that were saved from previous sessions
        LoadSettings();
    }
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

    private void LoadSettings()
    {
        // Load initial settings from PlayerPrefs  if they exist,
        // or load default values if PlayerPrefs don't exist

        // 1. Volume
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
        AudioListener.volume = masterVolume;

        // 2. Resolution
        
        //set a hardcoded default resolution if no PlayerPref exists
        int selectedResolutionIndex = PlayerPrefs.GetInt("SelectedResolution", -1);
        if (selectedResolutionIndex == -1) 
        {
            Screen.SetResolution(DEFAULT_RES_WIDTH, DEFAULT_RES_HEIGHT, Screen.fullScreen);
        }
        else
        {
            // Recreate the list of supported resolutions, use the saved index to retrieve resolution
            Resolution[] supportedResolutions = Screen.resolutions;
            Resolution selectedResolution = supportedResolutions[selectedResolutionIndex];
            // Actually change the resolution
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        }

        // 3. Language
        string languageIndexPref = PlayerPrefs.GetString("SelectedLanguage");
        // TODO change the language
    }
}
