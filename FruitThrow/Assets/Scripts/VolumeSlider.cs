using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(val => SoundManager.instance.ChangeMasterVolume(val));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
