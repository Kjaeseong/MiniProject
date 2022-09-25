using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class JS_GagueManagerTest : MonoBehaviour


{
    [SerializeField] // 배고픔 게이지 UI
    private TextMeshProUGUI _hungrygagueUI;
    [SerializeField] // 배고픔 UI 바
    private Image _gagueBar;

    [SerializeField] private TextMeshProUGUI _happyGagueUI;
    [SerializeField] private Image _happyGagueBar;

    public JS_BuffManager BuffManager;
    private float DecreaseTime = 10f;
    // 배고픔 수치
    
    // 먹이 살때 회복되는 양 (테스트용 public 추후 private 수정 예정)
    [Range(0, 100)]public int _hungryGague;
    // 행복게이지 (테스트용 public, 추후 private 수정 예정)
    [Range(0, 100)]public int _happyGague;


    public int RestoreAmount = 1;

    private void OnEnable()
    {
        GameManager.Instance.BuyFood.AddListener(RestoreHungryGague);
    }

    /// <summary>
    /// 먹이 살때 배고픔 게이지 회복시키는 함수
    /// </summary>
    private void RestoreHungryGague()
    {
        if(_hungryGague == 0)
        {
            GameManager.Instance.ContinueCoinSpawn();
        }
        _hungryGague += RestoreAmount;
        _hungrygagueUI.text = $"Hungry : {_hungryGague}";
        _gagueBar.fillAmount = (float)_hungryGague / 100;
    }

    private void Start()
    {
        _hungryGague = 100;
        StartCoroutine(HungryGague());
    }

    /// <summary>
    /// 10초마다 배고픔 게이지 감소(곰 개수 X 1)
    /// </summary>
    IEnumerator HungryGague()
    {
        while(true)
        {
            yield return new WaitForSeconds(DecreaseTime);
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

    private void OnDisable()
    {
        GameManager.Instance.BuyFood.RemoveListener(RestoreHungryGague);
    }


    private void Update() 
    {
        GagueUpdate();
    }

    private void GagueUpdate()
    {
        _hungrygagueUI.text = $"Hungry : {_hungryGague.ToString()}";
        _happyGagueUI.text = $"Happy : {_happyGague.ToString()}";
        _happyGagueBar.fillAmount = (float)_happyGague / 100;
        
        

        if(_happyGague >= 100)
        {
            BuffManager.EventStep = 4;
            _happyGagueBar.color = Color.yellow;
        }
        else if(_happyGague >= 90)
        {
            Debug.Log("Green / 피버 이벤트 / 코인 생산시간 절반, 생산코인 * 2 / Dance 모션");
            BuffManager.EventStep = 3;
            _happyGagueBar.color = Color.green;
            
        }
        else if(16 <= _happyGague && _happyGague <= 25)
        {
            Debug.Log("Orange / 앵그리곰이벤트 / 코인 생산시간 2배 / Idle모션 랜덤출력");
            BuffManager.EventStep = 2;
            _happyGagueBar.color = Color.cyan;
        }
        else if(1 <= _happyGague && _happyGague <= 15)
        {
            Debug.Log("Red / 가출 이벤트, 20초마다 랜덤으로 곰 -1 / Fade Out");
            BuffManager.EventStep = 1;
            _happyGagueBar.color = Color.red;
        }
        else if(0 >= _happyGague)
        {
            Debug.Log("Game Over");
        }
    }


}
