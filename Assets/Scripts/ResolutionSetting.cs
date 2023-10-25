using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown; // Reference to your dropdown UI component

    private void Start()
    {
        // Create a list of all resolutions supported by the player's machine
        Resolution[] resolutions = Screen.resolutions;
        List<string> resolutionOptions = new List<string>();

        foreach (Resolution res in resolutions)
        {
            string option = res.width + " x " + res.height;
            resolutionOptions.Add(option);
        }

        // Clear the ui placeholder resolution options
        resolutionDropdown.ClearOptions();

        // Add the options to the dropdown
        resolutionDropdown.AddOptions(resolutionOptions);
    }

    public void SetResolution(int resolutionIndex)
    {
        // When an option is selected in the dropdown, change the resolution
        Resolution[] resolutions = Screen.resolutions;
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution selectedResolution = resolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
            Debug.Log("Attempting to set resolution: " +selectedResolution.width+" " +selectedResolution.height);

            // Save the chosen resolution to PlayerPrefs
            PlayerPrefs.SetInt("ResolutionPrefKey", resolutionIndex);
            PlayerPrefs.Save();

            // Output for testing purposes
            Debug.Log("[ACTION] Selected Resolution: " + resolutionIndex);
            DebugPlayerPrefs();
        }
    }

    private void DebugPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            Debug.Log("PlayerPrefs - MasterVolume: " +  PlayerPrefs.GetFloat("MasterVolume"));
        }
        if (PlayerPrefs.HasKey("ResolutionPrefKey"))
        {
            Debug.Log("PlayerPrefs - Resolution: "  +  PlayerPrefs.GetInt("ResolutionPrefKey"));
        }
        if (PlayerPrefs.HasKey("Language"))
        {
            Debug.Log("PlayerPrefs - Language: " +  PlayerPrefs.GetString("Language"));
        }
    }
}
