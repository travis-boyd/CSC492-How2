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
        VolumeNumber.SetText($"{(Value * 100).ToString("N0")}");
    }
}
