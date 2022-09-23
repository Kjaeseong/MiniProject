using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GagueManager : MonoBehaviour
{
    [SerializeField] // ����� ������ UI
    private TextMeshProUGUI _hungrygagueUI;
    [SerializeField] // ����� UI ��
    private Image _gagueBar;

    private float DecreaseTime = 10f;
    // ����� ��ġ
    public int _hungryGague;
    // ���� �춧 ȸ���Ǵ� �� (�׽�Ʈ�� public ���� private ���� ����)
    public int RestoreAmount = 1;

    private void OnEnable()
    {
        GameManager.Instance.BuyFood.AddListener(RestoreHungryGague);
    }

    /// <summary>
    /// ���� �춧 ����� ������ ȸ����Ű�� �Լ�
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
    /// 10�ʸ��� ����� ������ ����(�� ���� X 1)
    /// </summary>
    IEnumerator HungryGague()
    {
        while(true)
        {
            yield return new WaitForSeconds(DecreaseTime);
            _hungryGague -= GameManager.Instance.BearCount;
            if (_hungryGague <= 0) // ���� �������°� ����
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
}
