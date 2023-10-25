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
        languageDropdown.onValueChanged.AddListener(SaveSelectedLanguage);
    }

    private void SaveSelectedLanguage(int selectedIndex)
    {
        // Get the selected language from the dropdown
        string selectedLanguage = languageDropdown.options[selectedIndex].text;

        // Save the selected language to PlayerPrefs
        PlayerPrefs.SetString("Language", selectedLanguage);
        PlayerPrefs.Save();

        // TODO 
        // actually change the language that's loaded

        Debug.Log("Selected Language: " + selectedLanguage);
    }
}
