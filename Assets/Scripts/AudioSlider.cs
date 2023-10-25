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

    }
}
