using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private static int DEFAULT_RES_WIDTH = 1920;
    private static int DEFAULT_RES_HEIGHT = 1080;

    public TextMeshProUGUI moveMediumBox;

    private void Start()
    {
        // Load any settings that were saved from previous sessions
        LoadSettings();
        LoadProgression();
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

    private void LoadProgression()
    {
        // Load progression from PlayerPrefs
        // Progression includes info on which stages
        // the player has already beaten. It determines
        // which stages are unlocked.

        // This could be a clever single efficient string, but this way is dummy proof?
        // When a level is completed successfully, we'll simply use PlayerPrefs to 
        // save that completion record to local data.
        int prog_movement_easy = PlayerPrefs.GetInt("prog_movement_easy", 0);
        int prog_movement_medium = PlayerPrefs.GetInt("prog_movement_medium", 0);
        int prog_movement_hard = PlayerPrefs.GetInt("prog_movement_hard", 0);
        int prog_health_easy = PlayerPrefs.GetInt("prog_health_easy", 0);
        int prog_health_medium = PlayerPrefs.GetInt("prog_health_medium", 0);
        int prog_health_hard = PlayerPrefs.GetInt("prog_health_hard", 0);
        int prog_damage_easy = PlayerPrefs.GetInt("prog_damage_easy", 0);
        int prog_damage_medium = PlayerPrefs.GetInt("prog_damage_medium", 0);
        int prog_damage_hard = PlayerPrefs.GetInt("prog_damage_hard", 0);
        

        // quick proof of concept
        if (prog_movement_easy == 0)
        {
            Color grayColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            moveMediumBox.color = grayColor;
            moveMediumBox.text = "LOCKED";
        }
    }
}
