using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private static int DEFAULT_RES_WIDTH = 1920;
    private static int DEFAULT_RES_HEIGHT = 1080;

    public Button button_1_1, button_1_2, button_1_3, button_2_1, button_2_2, button_2_3, button_3_1, button_3_2, button_3_3;

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
        
        PlayerPrefs.SetInt("progression_1_1", 1);

        int prog_1_1 = PlayerPrefs.GetInt("progression_1_1", 0);
        int prog_1_2 = PlayerPrefs.GetInt("progression_1_2", 0);
        int prog_1_3 = PlayerPrefs.GetInt("progression_1_3", 0);
        int prog_2_1 = PlayerPrefs.GetInt("progression_2_1", 0);
        int prog_2_2 = PlayerPrefs.GetInt("progression_2_2", 0);
        int prog_2_3 = PlayerPrefs.GetInt("progression_2_3", 0);
        int prog_3_1 = PlayerPrefs.GetInt("progression_3_1", 0);
        int prog_3_2 = PlayerPrefs.GetInt("progression_3_2", 0);
        int prog_3_3 = PlayerPrefs.GetInt("progression_3_3", 0);
        

        /* quick proof of concept
        public TextMeshProUGUI moveMediumBox;
        Color grayColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        if (prog_1_2 == 0)
        {
            moveMediumBox.color = grayColor;
            moveMediumBox.text = "LOCKED";
        }
        */

        // This is the dumbest possible way to do this lol

        // If [Basic Movement] hasn't been completed, lock [Health 101] and [Damage 101]
        if (prog_1_3  == 0) deactivateButton(button_2_1);
        if (prog_1_3  == 0) deactivateButton(button_3_1);

        // if [easy] hasn't been completed, lock [medium]
        if (prog_1_1  == 0) deactivateButton(button_1_2);
        if (prog_2_1  == 0) deactivateButton(button_2_2);
        if (prog_3_1  == 0) deactivateButton(button_3_2);

        // if [medium] hasn't been completed, lock [hard]
        if (prog_1_2  == 0) deactivateButton(button_1_3);
        if (prog_2_2  == 0) deactivateButton(button_2_3);
        if (prog_3_2  == 0) deactivateButton(button_3_3);
    }

    private void deactivateButton(Button button)
    {
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.color = Color.grey;
        buttonText.text = "LOCKED"; 
        
    }
}
