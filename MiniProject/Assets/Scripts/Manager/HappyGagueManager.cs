using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappyGagueManager : MonoBehaviour
{
    public enum State
    {
        DEFAULT,
        BEAR_BYE,
        ANGRY_BEAR,
        FEVER_TIME,
        CHANGE_BACKGROUND
    };

    [SerializeField]
    private Sprite[] _backgroundImage;
    [SerializeField]
    private Image _nowBackGroundImage;

    public int EventStep { get; set; }

    private bool _finishFeverTime;
    private bool _deleteBear;
    private bool _stopDeleteBear;
    private int _backGroundIndex;

    [SerializeField]
    private BearGroup BearGroup;

    [SerializeField]
    private GameObject _popUp;
    private PopUpUI _popUpScript;

    private bool _changeStatus;
    private float _elapsedTime;

    private void Awake()
    {
        _elapsedTime = 0.0f;
        _changeStatus = false;
        _backGroundIndex = 0;
        _nowBackGroundImage.sprite = _backgroundImage[_backGroundIndex];
        _finishFeverTime = true;

        _popUpScript = _popUp.GetComponent<PopUpUI>();
    }

    void Update()
    {
        if(EventStep == (int)State.ANGRY_BEAR)
        {
            _elapsedTime = 0.0f;
        }

        switch (EventStep)
        {
            case (int)State.BEAR_BYE:
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= 20.0f)
                {
                    _elapsedTime = 0.0f;
                    string[] arr = { "BearBye1", "BearBye2" };
                    PopUp(arr[Random.Range(0, 2)]);
                    BearGroup.DeleteBear();
                }
                break;

            case (int)State.ANGRY_BEAR:
                if (_changeStatus == false)
                {
                    GameManager.Instance.AngryBearStatus();

                    PopUp("AngryBear");
                }
                _changeStatus = true;
                break;

            case (int)State.FEVER_TIME:
                if (_finishFeverTime == true)
                {
                    StartCoroutine(FeverTime());
                }
                break;

            case (int)State.CHANGE_BACKGROUND:
                ++_backGroundIndex;
                if (_backGroundIndex >= 5)
                {
                    UIManager.Instance.GameClearUI();
                    break;
                }
                _nowBackGroundImage.sprite = _backgroundImage[_backGroundIndex];
                break;

            case (int)State.DEFAULT:
                _changeStatus = false;
                break;
        }

    }
    private void CooltimeFeverTime()
    {
        _finishFeverTime = true;
    }

    IEnumerator BearGoodBye()
    {
        yield return new WaitForSeconds(20f);

        
    }

    IEnumerator FeverTime()
    {
        PopUp("FeverTime");
        _finishFeverTime = false;
        UIManager.Instance.AboutFeverUI();
        UIManager.Instance.ChangeInterection();
        GameManager.Instance.ChangeStatus();
        GameManager.Instance.StartEventTime();
        GameManager.Instance.GetCoinAmount = GameManager.Instance.StandardCoinAmount * 2;
        yield return new WaitForSeconds(30f);

        UIManager.Instance.AboutFeverUI();
        UIManager.Instance.ChangeInterection();
        GameManager.Instance.ChangeStatus();
        GameManager.Instance.GetCoinAmount = GameManager.Instance.StandardCoinAmount;
        Invoke("CooltimeFeverTime", 30f);
    }

    private void PopUp(string description)
    {
        _popUp.SetActive(true);
        _popUpScript.PopUpText(description);
    }
}
