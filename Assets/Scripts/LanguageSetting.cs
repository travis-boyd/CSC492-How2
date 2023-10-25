using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageSelection : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    private const string PlayerPrefKey = "SelectedLanguage";

    private void Start()
    {
        // Load the previously selected language from PlayerPrefs (if available)
        int savedLanguageIndex = PlayerPrefs.GetInt("Language", 0);
        languageDropdown.value = savedLanguageIndex;

        // Add a listener to the dropdown's onValueChanged event
        languageDropdown.onValueChanged.AddListener(SelectLanguage);
    }

    private void SelectLanguage(int selectedIndex)
    {
        // Get the selected language from the dropdown
        string selectedLanguage = languageDropdown.options[selectedIndex].text;

        // Save the selected language to PlayerPrefs
        PlayerPrefs.SetString("Language", selectedLanguage);
        PlayerPrefs.Save();

        // Output for testing purposes
        Debug.Log("[ACTION] Selected Language: " + selectedLanguage);
        DebugPlayerPrefs();


        // TODO 
        // actually change the language that's loaded

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
