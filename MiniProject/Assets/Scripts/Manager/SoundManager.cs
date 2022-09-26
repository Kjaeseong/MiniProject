using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Range(0, 100)]public float BgmVolume = 50f;
    [Range(0, 100)]public float SeVolume = 50f;

    private AudioSource Bgm;
    private AudioSource Se;
    private AudioSource Click;
    public List<AudioClip> BgmList = new List<AudioClip>();
    public List<AudioClip> SeList = new List<AudioClip>();
    public List<AudioClip> ClickList = new List<AudioClip>();


    private void Start() 
    {
        DontDestroyOnLoad(gameObject);
        Bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        Se = GameObject.Find("SE").GetComponent<AudioSource>();
        Click = GameObject.Find("CLICK").GetComponent<AudioSource>();
        BgmPlay(0);
    }

    public void BgmPlay(int AudioTrack)
    {
        if(BgmList.Count > 0)
        {
            Bgm.Stop();
            Bgm.clip = BgmList[AudioTrack];
            Bgm.Play();
        }
    }
    
    public void BgmStop()
    {
        Bgm.Stop();
    }

    public void SePlay(int AudioTrack)
    {
        if(SeList.Count > 0)
        {
            Se.Stop();
            Se.clip = SeList[AudioTrack];
            Se.Play();
        }
    }

    public void SeStop()
    {
        Se.Stop();
    }

    public void ClickPlay(int AudioTrack)
    {
        if(ClickList.Count > 0)
        {
            Click.Stop();
            Click.clip = ClickList[AudioTrack];
            Click.Play();
        }

    }
    public void ClickStop()
    {
        Click.Stop();
    }

    
}
