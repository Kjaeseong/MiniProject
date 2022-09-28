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

    [SerializeField] private TextMeshProUGUI _happyGagueUI;
    [SerializeField] private Image _happyGagueBar;

    public HappyGagueManager HappyGaugeManagement;
    private float DecreaseTime = 10f;
    private Color _standardColor;

    [SerializeField]
    private GameObject _popUp;
    private PopUpUI _popUpScript;

    // ����� ��ġ
    // ���� �춧 ȸ���Ǵ� �� (�׽�Ʈ�� public ���� private ���� ����)
    [Range(0, 100)] public int _hungryGague;
    // �ູ������ (�׽�Ʈ�� public, ���� private ���� ����)
    [Range(0, 100)] public int _happyGague;

    private bool _stopEat = true;
    private bool _coinStop;

    private bool _isGameOver;

    
    public int RestoreAmount = 1;

    private void OnEnable()
    {
        GameManager.Instance.BuyFood.AddListener(RestoreHungryGague);
        _popUpScript = _popUp.GetComponent<PopUpUI>();
    }

    private void Start()
    {
        _standardColor = _happyGagueBar.color;
        _hungryGague = 100;
        Invoke("StartReduce",3f);
        Invoke("CheckGague", 3f);
    }

    /// <summary>
    /// ���� �춧 ����� ������ ȸ����Ű�� �Լ�
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
    /// 3�ʵڿ� ����� ������ ���� ����
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

        HungryCheck();
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
            _happyGague = 50;
            _happyGagueBar.color = Color.yellow;
        }
        else if (_happyGague >= 90 && _happyGague < 100)
        {
            Debug.Log("Green / �ǹ� �̺�Ʈ / ���� ����ð� ����, �������� * 2 / Dance ���");
            HappyGaugeManagement.EventStep = (int)HappyGagueManager.State.FEVER_TIME;
            _happyGagueBar.color = Color.green;

        }
        else if (16 <= _happyGague && _happyGague <= 25)
        {
            Debug.Log("Orange / �ޱ׸����̺�Ʈ / ���� ����ð� 2�� / Idle��� �������");
            HappyGaugeManagement.EventStep = (int)HappyGagueManager.State.ANGRY_BEAR;
            _happyGagueBar.color = Color.cyan;
        }
        else if (1 <= _happyGague && _happyGague <= 15)
        {
            Debug.Log("Red / ���� �̺�Ʈ, 20�ʸ��� �������� �� -1 / Fade Out");
            HappyGaugeManagement.EventStep = (int)HappyGagueManager.State.BEAR_BYE;
            _happyGagueBar.color = Color.red;
        }
        else if (0 >= _happyGague)
        {
            if(!_isGameOver)
            {
                UIManager.Instance.GameOverUI();
                _isGameOver = true;
            }
        }

        if (25 < _happyGague && _happyGague <=100)
        {
            GameManager.Instance.RollbackBearStatus();
        }
    }

    /// <summary>
    /// 10�ʸ��� ����� ������ ����(�� ���� X 1)
    /// </summary>
    IEnumerator HungryGague()
    {
        while (true)
        {
            yield return new WaitForSeconds(DecreaseTime);
            // �̺�Ʈ Ÿ�� �ƴҶ� ����
            if (GameManager.Instance.IsEventTime == false)
            {
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
    }

    IEnumerator CheckHungryGague()
    {
        while(true)
        {
            yield return new WaitForSeconds(10f);
            // �̺�Ʈ Ÿ�� �ƴҶ� ����
            if (GameManager.Instance.IsEventTime == false)
            {
                if (_hungryGague >= 90)
                {
                    PopUp("HappyGagueUp");
                    _happyGague += 10;
                    if (_happyGague > 100)
                    {
                        _happyGague = 100;
                    }
                }
                else if (_hungryGague <= 25)
                {
                    PopUp("HappyGagueDown");
                    _happyGague -= 10;
                    if (_happyGague < 0)
                    {
                        _happyGague = 0;
                    }
                }
            }
        }
    }

    private void HungryCheck()
    {
        if(_hungryGague <= 0)
        {
            if(!_coinStop)
            {
                PopUp("CoinStop");
                _coinStop = true;
            }
        }
        else
        {
            _coinStop = false;
        }

        if(_hungryGague >= 100)
        {
            if(!_stopEat)
            {
                PopUp("StopEat");
                _stopEat = true;
            }
        }
        else
        {
            _stopEat = false;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.BuyFood.RemoveListener(RestoreHungryGague);
    }

    private void PopUp(string description)
    {
        _popUp.SetActive(true);
        _popUpScript.PopUpText(description);
    }

}
