using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GagueManager : MonoBehaviour
{
    [SerializeField] // 배고픔 게이지 UI
    private TextMeshProUGUI _hungrygagueUI;
    [SerializeField] // 배고픔 UI 바
    private Image _gagueBar;

    [SerializeField] private TextMeshProUGUI _happyGagueUI;
    [SerializeField] private Image _happyGagueBar;

    public HappyGagueManager HappyGaugeManagement;
    private float DecreaseTime = 10f;
    private Color _standardColor;

    // 배고픔 수치
    // 먹이 살때 회복되는 양 (테스트용 public 추후 private 수정 예정)
    [Range(0, 100)] public int _hungryGague;
    // 행복게이지 (테스트용 public, 추후 private 수정 예정)
    [Range(0, 100)] public int _happyGague;


    public int RestoreAmount = 1;

    private void OnEnable()
    {
        GameManager.Instance.BuyFood.AddListener(RestoreHungryGague);
    }

    private void Start()
    {
        _standardColor = _happyGagueBar.color;
        _hungryGague = 100;
        Invoke("StartReduce",3f);
        Invoke("CheckGague", 3f);
    }

    /// <summary>
    /// 먹이 살때 배고픔 게이지 회복시키는 함수
    /// </summary>
    private void RestoreHungryGague()
    {
        if (_hungryGague == 0)
        {
            GameManager.Instance.ContinueCoinSpawn();
        }
        _hungryGague += RestoreAmount;
        // _hungrygagueUI.text = $"Hungry : {_hungryGague}";
        _gagueBar.fillAmount = (float)_hungryGague / 100;
    }

    /// <summary>
    /// 3초뒤에 배고픔 게이지 감소 시작
    /// </summary>
    private void StartReduce()
    {
        StartCoroutine(HungryGague());
    }

    private void CheckGague()
    {
        StartCoroutine(CheckHungryGague());
    }

    private void Update()
    {
        GagueUpdate();

        _hungrygagueUI.text = $"Hungry : {_hungryGague}";
        _gagueBar.fillAmount = (float)_hungryGague / 100;
    }

    private void GagueUpdate()
    {
        _happyGagueUI.text = $"Happy : {_happyGague}";
        _happyGagueBar.fillAmount = (float)_happyGague / 100;
        _happyGagueBar.color = _standardColor;

        HappyGaugeManagement.EventStep = (int)HappyGagueManager.State.DEFAULT;

        if (_happyGague >= 100)
        {
            HappyGaugeManagement.EventStep = (int)HappyGagueManager.State.CHANGE_BACKGROUND;
            _happyGagueBar.color = Color.yellow;
        }
        else if (_happyGague >= 90 && _happyGague < 100)
        {
            Debug.Log("Green / 피버 이벤트 / 코인 생산시간 절반, 생산코인 * 2 / Dance 모션");
            HappyGaugeManagement.EventStep = (int)HappyGagueManager.State.FEVER_TIME;
            _happyGagueBar.color = Color.green;

        }
        else if (16 <= _happyGague && _happyGague <= 25)
        {
            Debug.Log("Orange / 앵그리곰이벤트 / 코인 생산시간 2배 / Idle모션 랜덤출력");
            HappyGaugeManagement.EventStep = (int)HappyGagueManager.State.ANGRY_BEAR;
            GameManager.Instance.AngryBearStatus();
            _happyGagueBar.color = Color.cyan;
        }
        else if (1 <= _happyGague && _happyGague <= 15)
        {
            Debug.Log("Red / 가출 이벤트, 20초마다 랜덤으로 곰 -1 / Fade Out");
            HappyGaugeManagement.EventStep = (int)HappyGagueManager.State.BEAR_BYE;
            _happyGagueBar.color = Color.red;
        }
        else if (0 >= _happyGague)
        {
            Debug.Log("Game Over");
        }

        if (16 >= _happyGague && _happyGague <=100)
        {
            GameManager.Instance.RollbackBearStatus();
        }
    }

    /// <summary>
    /// 10초마다 배고픔 게이지 감소(곰 개수 X 1)
    /// </summary>
    IEnumerator HungryGague()
    {
        while (true)
        {
            yield return new WaitForSeconds(DecreaseTime);
            // 이벤트 타임 아닐때 감소
            if (GameManager.Instance.IsEventTime == false)
            {
                _hungryGague -= GameManager.Instance.BearCount;
                if (_hungryGague <= 0) // 음수 떨어지는거 방지
                {
                    _hungryGague = 0;
                    GameManager.Instance.StopSpawnCoin();
                }
                _hungrygagueUI.text = $"Hungry : {_hungryGague}";
                _gagueBar.fillAmount = (float)_hungryGague / 100;
            }
        }
    }

    IEnumerator CheckHungryGague()
    {
        while(true)
        {
            yield return new WaitForSeconds(10f);
            // 이벤트 타임 아닐때 감소
            if (GameManager.Instance.IsEventTime == false)
            {
                if (_hungryGague >= 90)
                {
                    _happyGague += 10;
                    if (_happyGague > 100)
                    {
                        _happyGague = 100;
                    }
                }
                else if (_hungryGague <= 25)
                {
                    _happyGague -= 10;
                    if (_happyGague < 0)
                    {
                        _happyGague = 0;
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.BuyFood.RemoveListener(RestoreHungryGague);
    }

}
