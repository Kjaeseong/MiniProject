using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    [SerializeField]
    private GameObject FeverUI;

    public bool ShowFeverUI { get; private set; }

    private void Start()
    {
        ShowFeverUI = false;
    }
    /// <summary>
    /// FeverUI���� �Լ�
    /// </summary>
    public void AboutFeverUI()
    {
        ShowFeverUI = !ShowFeverUI;
        FeverUI.SetActive(ShowFeverUI);
    }
}
