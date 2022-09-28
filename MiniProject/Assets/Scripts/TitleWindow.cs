using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWindow : MonoBehaviour
{
    public SoundManager _sound;
    private void OnEnable() 
    {
        _sound.BgmPlay(0);
    }
}
