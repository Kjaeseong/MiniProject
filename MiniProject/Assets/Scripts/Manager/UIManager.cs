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

    public bool ShowFeverUI { get; private set; }
    public bool ShowPauseMenuUI { get; private set; }

    private void Start()
    {
        ShowFeverUI = false;
        ShowPauseMenuUI = false;
    }
    /// <summary>
    /// FeverUI관련 함수
    /// </summary>
    public void AboutFeverUI()
    {
        ShowFeverUI = !ShowFeverUI;
        _feverUI.SetActive(ShowFeverUI);
    }
    /// <summary>
    /// 메뉴 닫기
    // </summary>
    public void Menu()
    {
        ShowPauseMenuUI = !ShowPauseMenuUI;
        if (ShowPauseMenuUI == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        _menuUI.SetActive(ShowPauseMenuUI);
    }

    /// <summary>
    /// 게임 클리어 UI 출력
    /// </summary>
    public void GameClearUI()
    {
        Time.timeScale = 0f;
        _clearUI.SetActive(true);
    }

    /// <summary>
    /// 게임오버 UI 출력
    /// </summary>
    public void GameOverUI()
    {
        Time.timeScale = 0f;
        _gameoverUI.SetActive(true);
    }

    /// <summary>
    /// 타이틀화면으로 돌아가기
    /// </summary>
    public void GoToTitleScene()
    {
        Time.timeScale = 1f;
        _clearUI.SetActive(false);
        _gameoverUI.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
