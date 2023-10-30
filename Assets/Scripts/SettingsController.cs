using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// TODO:
// - save resolution as absolute values instead of a list index
// - save language as absolute value instead of a list index

public class SettingsController : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    public TMP_Dropdown resolutionDropdown;


    // Start is called before the first frame update
    void Start()
    {

        // Create a list of Resolution objects of supported resolutions
        // and populate ResolutionDropdown with the list
        Resolution[] resolutions = Screen.resolutions;
        List<string> resolutionOptions = new List<string>();
        foreach (Resolution res in resolutions)
        {
            string option = res.width + " x " + res.height;
            resolutionOptions.Add(option);
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionOptions);



        // Load the previously saved values from PlayerPrefs (if available)
        // set them as the currently selected indexes
        int savedLanguageIndex = PlayerPrefs.GetInt("LanguageIndexPref", 0);
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndexPref, 0");
        languageDropdown.value = savedLanguageIndex;
        resolutionDropdown.value = savedResolutionIndex;

        // Add listener's to the dropdowns' onValueChanged events
        languageDropdown.onValueChanged.AddListener(SelectLanguage);
        resolutionDropdown.onValueChanged.AddListener(SelectResolution);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectLanguage(int selectedIndex)
    {
        // Get the selected language from the dropdown
        string selectedLanguage = languageDropdown.options[selectedIndex].text;
        
        // TODO: actually change the language

        // Save the selected language to PlayerPrefs
        PlayerPrefs.SetInt("LanguageIndexPref", selectedIndex);
        PlayerPrefs.Save();

        // Output for testing purposes
        Debug.Log("[ACTION] Selected Language: " + selectedLanguage + "(" + selectedIndex + ")");
        DebugPlayerPrefs();


    }

    private void SelectResolution(int selectedIndex)
    {
        // Get the selected resolution from the dropdown
        Resolution[] supportedResolutions = Screen.resolutions;
        Resolution selectedResolution = supportedResolutions[selectedIndex];

        // Actually change the resolution
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        // Save the selected resolution to PlayerPrefs
        PlayerPrefs.SetInt("ResolutionIndexPref", selectedIndex);

        // Output for testing purposes
        Debug.Log("[ACTION] Selected Resolution: " + selectedIndex);
        DebugPlayerPrefs(); 
    }


    // Method to output all saved PlayerPrefs
    private void DebugPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            Debug.Log("PlayerPrefs - MasterVolume: " +  PlayerPrefs.GetFloat("MasterVolume"));
        }
        if (PlayerPrefs.HasKey("ResolutionIndexPref"))
        {
            Debug.Log("PlayerPrefs - ResolutionIndexPref: "  +  PlayerPrefs.GetInt("ResolutionIndexPref"));
        }
        if (PlayerPrefs.HasKey("LanguageIndexPref"))
        {
            Debug.Log("PlayerPrefs - LanguageIndexPref: " +  PlayerPrefs.GetInt("LanguageIndexPref"));
        }
    }
}
