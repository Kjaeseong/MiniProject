using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject OptionWindow;
    public GameObject TitleWindow;
    public GameObject CreditWindow;

    private void Start() 
    {
        OptionWindow.SetActive(false);
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("TItleScene");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void OptionScene()
    {
        OptionWindow.SetActive(true);
        TitleWindow.SetActive(false);
    }
}
