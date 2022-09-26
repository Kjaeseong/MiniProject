using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject _optionWindow;
    public GameObject _titleWindow;

    private void Start() 
    {
        _optionWindow.SetActive(false);
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
        _optionWindow.SetActive(true);
        _titleWindow.SetActive(false);
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
