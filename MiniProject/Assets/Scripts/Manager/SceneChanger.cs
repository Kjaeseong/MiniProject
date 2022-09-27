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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CloseCreditScene();
        }
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

    public void CreditScene()
    {
        CreditWindow.SetActive(true);
        TitleWindow.SetActive(false);
    }

    public void CloseCreditScene()
    {
        CreditWindow.SetActive(false);
        TitleWindow.SetActive(true);
    }
}
