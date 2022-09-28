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
    public VolumeManager VolumeManager;

    private void OnEnable() 
    {
        BgmSlider.value = SoundManagement.BgmVolume;
        SeSlider.value = SoundManagement.SeVolume;
        _titleWindow.SetActive(false);
    }

    void FixedUpdate()
    {
        SoundManagement.BgmVolume = BgmSlider.value;
        SoundManagement.SeVolume = SeSlider.value;
        VolumeManager.BgmVolume = BgmSlider.value;
        VolumeManager.SeVolume = SeSlider.value;

        SoundManagement.ChangeVolume();
    }

    public void DeActivate()
    {
        _titleWindow.SetActive(true);
        gameObject.SetActive(false);
    }
}
