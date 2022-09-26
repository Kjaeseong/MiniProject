using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    public Slider BgmSlider;
    public Slider SeSlider;

    public GameObject _titleWindow;
    public SoundManager SoundManagement;

    private void OnEnable() 
    {
        BgmSlider.value = SoundManagement.BgmVolume / 100;
        SeSlider.value = SoundManagement.SeVolume / 100;
        _titleWindow.SetActive(false);
    }

    void FixedUpdate()
    {
        SoundManagement.BgmVolume = BgmSlider.value * 100;
        SoundManagement.SeVolume = SeSlider.value * 100;
    }

    public void DeActivate()
    {
        _titleWindow.SetActive(true);
        gameObject.SetActive(false);
    }
}
