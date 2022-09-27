using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonBehaviour<UIManager>
{
    [SerializeField]
    private GameObject _feverUI;
    [SerializeField]
    private GameObject _menuUI;
    [SerializeField]
    private GameObject _clearUI;
    [SerializeField]
    private GameObject _gameoverUI;
    [SerializeField]
    private GameObject _popUpUI;
    private PopUpUI _popUpUiScript;
    [SerializeField]
    private SoundManager _sound;

    public bool ShowFeverUI { get; private set; }
    public bool ShowPauseMenuUI { get; private set; }

    private void Start()
    {
        ShowFeverUI = false;
        ShowPauseMenuUI = false;
    }
    /// <summary>
    /// FeverUI���� �Լ�
    /// </summary>
    public void AboutFeverUI()
    {
        ShowFeverUI = !ShowFeverUI;
        _feverUI.SetActive(ShowFeverUI);

        //�߰� �ڵ�
        _sound.BgmPlay(2);
    }

    /// <summary>
    /// �޴� �ݱ�
    // </summary>
    public void Menu()
    {
        ShowPauseMenuUI = !ShowPauseMenuUI;
        if (ShowPauseMenuUI == true)
        {
            Time.timeScale = 0f;
            _sound.ClickPlay(1);
        }
        else
        {
            Time.timeScale = 1f;
        }
        _menuUI.SetActive(ShowPauseMenuUI);
    }

    /// <summary>
    /// ���� Ŭ���� UI ���
    /// </summary>
    public void GameClearUI()
    {
        Time.timeScale = 0f;
        _clearUI.SetActive(true);
        _sound.BgmPlay(4);
    }

    /// <summary>
    /// ���ӿ��� UI ���
    /// </summary>
    public void GameOverUI()
    {
        Time.timeScale = 0f;
        _gameoverUI.SetActive(true);
        _sound.BgmPlay(3);
    }

    /// <summary>
    /// Ÿ��Ʋȭ������ ���ư���
    /// </summary>
    public void GoToTitleScene()
    {
        Time.timeScale = 1f;
        _clearUI.SetActive(false);
        _gameoverUI.SetActive(false);
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// �˾�â UI ȣ��. �Ű������� �̺�Ʈ�� �Է�(CSV���� ����)
    /// </summary>
    public void PopUpUI(string description)
    {
        if(_popUpUiScript != null)
        {
            _popUpUiScript = _popUpUI.GetComponent<PopUpUI>();
        }

        Time.timeScale = 0f;
        _popUpUI.SetActive(true);
        _popUpUiScript.PopUpText(description);
        _sound.SePlay(2);
    }
}
