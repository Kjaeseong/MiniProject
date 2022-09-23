using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JS_LoadingBar : MonoBehaviour
{
    public Slider LoadingBar;
    public SceneChanger Scene;
    public Image Gauge;

    private void Update() 
    {
        LoadingBar.value += Time.deltaTime / 3.3f;
        Gauge.fillAmount = LoadingBar.value;

        if(LoadingBar.value >= 1) { 
            NextScene(); 
        }
    }

    private void NextScene()
    {
        Scene.TitleScene();
    }
}
