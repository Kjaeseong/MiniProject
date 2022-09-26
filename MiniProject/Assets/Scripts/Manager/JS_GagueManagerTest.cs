using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class JS_GagueManagerTest : MonoBehaviour


{
    [SerializeField] // ����� ������ UI
    private TextMeshProUGUI _hungrygagueUI;
    [SerializeField] // ����� UI ��
    private Image _gagueBar;

    [SerializeField] private TextMeshProUGUI _happyGagueUI;
    [SerializeField] private Image _happyGagueBar;

    public JS_BuffManager BuffManager;
    private float DecreaseTime = 10f;
    // ����� ��ġ
    
    // ���� �춧 ȸ���Ǵ� �� (�׽�Ʈ�� public ���� private ���� ����)
    [Range(0, 100)]public int _hungryGague;
    // �ູ������ (�׽�Ʈ�� public, ���� private ���� ����)
    [Range(0, 100)]public int _happyGague;


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
            Debug.Log("Green / �ǹ� �̺�Ʈ / ���� ����ð� ����, �������� * 2 / Dance ���");
            BuffManager.EventStep = 3;
            _happyGagueBar.color = Color.green;
            
        }
        else if(16 <= _happyGague && _happyGague <= 25)
        {
            Debug.Log("Orange / �ޱ׸����̺�Ʈ / ���� ����ð� 2�� / Idle��� �������");
            BuffManager.EventStep = 2;
            _happyGagueBar.color = Color.cyan;
        }
        else if(1 <= _happyGague && _happyGague <= 15)
        {
            Debug.Log("Red / ���� �̺�Ʈ, 20�ʸ��� �������� �� -1 / Fade Out");
            BuffManager.EventStep = 1;
            _happyGagueBar.color = Color.red;
        }
        else if(0 >= _happyGague)
        {
            Debug.Log("Game Over");
        }
    }


}
