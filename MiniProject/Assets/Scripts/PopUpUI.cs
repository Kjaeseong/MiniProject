using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpUI : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public List<string> TextList = new List<string>();

    private void OnEnable() 
    {
        Time.timeScale = 0;
        Text.text = "GGGGGGGGGGGGGG";
    }

    private void OnDisable() 
    {
        Time.timeScale = 1;
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
