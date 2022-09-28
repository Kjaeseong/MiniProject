using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditWindow : MonoBehaviour
{
    public GameObject TitleWindow;
    public SoundManager SoundManager;

    private void OnEnable() 
    {
        SoundManager.SePlay(4);
    }

    private void OnDisable()
    {
        TitleWindow.SetActive(true);
        SoundManager.SeStop();
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
