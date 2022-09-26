using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    public Slider BgmSlider;
    public Slider SeSlider;

    public GameObject _titleWindow;

    //GameManager Script -> float BgmVolume, float SeVolume 변수 추가
    private void OnEnable() 
    {
        // 게임 적용시 아래 코드 주석 해제.
        //BgmSlider.value = GameManager.Instance.BgmVolume / 100;
        //SeSlider.value = GameManager.Instance.SeVolume / 100;
        _titleWindow.SetActive(false);
    }

    void FixedUpdate()
    {
        // 게임 적용시 아래 코드 주석 해제.
        // GameManager.Instance.BgmVolume = BgmSlider.value * 100;
        // GameManager.Instance.SeVolume = SeSlider.value * 100;
    }

    public void DeActivate()
    {
        _titleWindow.SetActive(true);
        gameObject.SetActive(false);
    }
}
