using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void MainScene()
    {
        SceneManager.LoadScene("ParkDoilScene");
    }

    public void OptionScene()
    {
        SceneManager.LoadScene("OptionScene");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
}