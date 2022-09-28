using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Range(0, 1)]public float BgmVolume;
    [Range(0, 1)]public float SeVolume;

    [Space(9)]
    public AudioSource Bgm;
    public List<AudioClip> BgmList = new List<AudioClip>();
    [Space(9)]
    public AudioSource Se;
    public List<AudioClip> SeList = new List<AudioClip>();
    [Space(9)]
    public AudioSource Click;
    public List<AudioClip> ClickList = new List<AudioClip>();

    public VolumeManager _volume;

    private void Start() 
    {
        _volume = GameObject.Find("VolumeManager").GetComponent<VolumeManager>();
        BgmVolume = _volume.BgmVolume;
        SeVolume = _volume.SeVolume;

        ChangeVolume();
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

    public void BgmPause()
    {
        Bgm.Pause();
        Se.Pause();
    }

    public void BgmUnPause()
    {
        Bgm.UnPause();
        Se.UnPause();
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

    public void ChangeVolume()
    {
        Bgm.volume = BgmVolume;
        Se.volume = SeVolume;
        Click.volume = SeVolume;
    }
}
