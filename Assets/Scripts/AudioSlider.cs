using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI VolumeNumber;
    

    public void onChangeSlider(float Value)
    {

        // Save selected volume to PlayerPrefs
        PlayerPrefs.SetFloat("MasterVolume", Value);
        PlayerPrefs.Save();

        // Update volume
        AudioListener.volume = Value;

        // Update UI
        VolumeNumber.SetText($"{(Value * 100).ToString("N0")}");

        // Output for testing purposes
        Debug.Log("[ACTION] Selected MasterVolume: " + Value);
        DebugPlayerPrefs();

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
