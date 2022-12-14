using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private Button _buyBearButton;
    [SerializeField]
    private Button _buyFoodButton;

    public bool ShowFeverUI { get; private set; }
    public bool ShowPauseMenuUI { get; private set; }
    public bool IsInterective { get; private set; }

    private void Start()
    {
        ShowFeverUI = false;
        ShowPauseMenuUI = false;
        IsInterective = true;
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
            _sound.BgmPause();
            _sound.ClickPlay(1);
        }
        else
        {
            Time.timeScale = 1f;
            _sound.BgmUnPause();
        }
        _menuUI.SetActive(ShowPauseMenuUI);
    }

    /// <summary>
    /// 게임 클리어 UI 출력
    /// </summary>
    public void GameClearUI()
    {
        _sound.BgmPlay(4);
        _clearUI.SetActive(true);
    }

    /// <summary>
    /// 게임오버 UI 출력
    /// </summary>
    public void GameOverUI()
    {
        _sound.BgmPlay(3);
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
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 팝업창 UI 호출. 매개변수로 이벤트명 입력(CSV파일 참고)
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

    public void ChangeInterection()
    {
        IsInterective = !IsInterective;
        _pauseButton.interactable = IsInterective;
        _buyBearButton.interactable = IsInterective;
        _buyFoodButton.interactable = IsInterective;

    }
}
