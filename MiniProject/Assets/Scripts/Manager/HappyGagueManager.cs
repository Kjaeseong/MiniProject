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
    private int _backGroundIndex;

    public BearGroup BearGroup;

    private void Awake()
    {
        _backGroundIndex = 0;
        _nowBackGroundImage.sprite = _backgroundImage[_backGroundIndex];
        _deleteBear = false;
        _finishFeverTime = true;
    }

    void Update()
    {
        switch (EventStep)
        {
            case (int)State.BEAR_BYE:
                if (_deleteBear == false)
                {
                    StartCoroutine(BearGoodBye());
                }
                _deleteBear = true;
                break;

            case (int)State.ANGRY_BEAR:
                GameManager.Instance.AngryBearStatus();
                break;

            case (int)State.FEVER_TIME:
                if(_finishFeverTime == true)
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
                StopCoroutine(BearGoodBye());
                break;
        }

    }
    private void FinishFeverTime()
    {
        _finishFeverTime = true;
    }

    IEnumerator BearGoodBye()
    {
        yield return new WaitForSeconds(20f);
        BearGroup.DeleteBear();
        _deleteBear = false;
    }

    IEnumerator FeverTime()
    {
        _finishFeverTime = false;
        UIManager.Instance.AboutFeverUI();
        GameManager.Instance.ChangeStatus();
        GameManager.Instance.StartEventTime();
        GameManager.Instance.GetCoinAmount = GameManager.Instance.StandardCoinAmount * 2;
        yield return new WaitForSeconds(30f);

        UIManager.Instance.AboutFeverUI();
        GameManager.Instance.ChangeStatus();
        GameManager.Instance.GetCoinAmount = GameManager.Instance.StandardCoinAmount;
        Invoke("FinishFeverTime", 20f);
    }
}
