using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public float BgmVolume = 0.5f;
    public float SeVolume = 0.5f;

    private void Start() 
    {
        DontDestroyOnLoad(gameObject);
    }
}
