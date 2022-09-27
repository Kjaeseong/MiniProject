using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverTimeUI : MonoBehaviour
{
    public SoundManager _sound;
    private void OnEnable() 
    {
        _sound.BgmPlay(2);
    }

    private void OnDisable() 
    {
        _sound.BgmPlay(1);
    }
}
