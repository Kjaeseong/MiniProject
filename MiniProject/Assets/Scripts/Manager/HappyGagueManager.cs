using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int EventStep { get; set; }

    private bool _finishFeverTime;

    private void Awake()
    {
        _finishFeverTime = true;
    }

    void Update()
    {
        switch (EventStep)
        {
            case (int)State.BEAR_BYE:
                StartCoroutine(BearGoodBye());
                break;
            case (int)State.ANGRY_BEAR:
                break;
            case (int)State.FEVER_TIME:
                if(_finishFeverTime == true)
                {
                    StartCoroutine(FeverTime());
                }
                break;
            case (int)State.CHANGE_BACKGROUND:
                break;
            case (int)State.DEFAULT:
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
