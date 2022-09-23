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

    // 배고픔 수치
    public int _hungryGague;

    // 먹이 살때 회복되는 양 (테스트용 public 추후 private 수정 예정)
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
    /// 60초마다 배고픔 게이지 감소(곰 개수 X 1)
    /// </summary>
    IEnumerator HungryGague()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f);
            _hungryGague -= GameManager.Instance.BearCount;
            _hungrygagueUI.text = $"Hungry : {_hungryGague}";
            _gagueBar.fillAmount = (float)_hungryGague / 100;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.BuyFood.RemoveListener(RestoreHungryGague);
    }
}
