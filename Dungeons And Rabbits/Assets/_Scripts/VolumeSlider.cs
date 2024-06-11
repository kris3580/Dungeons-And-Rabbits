using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = MiscellaneousEvents.volume;
    }

    void Update()
    {
        MiscellaneousEvents.volume = volumeSlider.value;
    }
}
