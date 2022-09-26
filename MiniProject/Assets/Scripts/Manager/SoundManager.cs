using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Range(0, 100)]public float BgmVolume = 50f;
    [Range(0, 100)]public float SeVolume = 50f;

    public List<AudioClip> BgmList = new List<AudioClip>();
    public List<AudioClip> SeList = new List<AudioClip>();

    private void Start() 
    {
        DontDestroyOnLoad(gameObject);
        BgmPlay();
    }

    private void BgmPlay()
    {

    }

    public void SePlay(string SeFileName)
    {
        
    }

    
}
