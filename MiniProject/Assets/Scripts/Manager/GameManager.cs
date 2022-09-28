using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public UnityEvent BuyFood = new UnityEvent();
    public UnityEvent StopCoin = new UnityEvent();
    public UnityEvent RestartCoin = new UnityEvent();
    public UnityEvent EventTime = new UnityEvent();
    public UnityEvent AngryBear = new UnityEvent();
    public UnityEvent NormalBear = new UnityEvent();

    [SerializeField] //�� ������Ʈ
    private GameObject _bear;
    [SerializeField] // �� ���� ��ġ
    private GameObject[] _spawnPositions;
    [SerializeField] // Gold UI
    private TextMeshProUGUI _goldUI;
    [SerializeField] // �� ���� UI
    private TextMeshProUGUI _bearPriceUI;
    [SerializeField] // ���� ���� UI
    private TextMeshProUGUI _foodPriceUI;
    [SerializeField] // ������ �Ŵ���
    private GagueManager _gagueManager;
    [SerializeField] // �Ͻ������޴�
    private GameObject _pauseMenu;
    [SerializeField]
    private GameObject _positions;
    public BearGroup BearGroup;

    public int RemainCoin { get; private set; }

    public int GetCoinAmount = 100;
    public int StandardCoinAmount { get; private set; }
    private int BearPrice = 80;
    private int _foodPrice;

    public bool IsShowMenu { get; private set; }

    public int BearCount { get; set; }

    public bool IsEventTime { get; private set; }

    private void Start()
    {
        IsEventTime = false;
        _foodPrice = BearCount * 5;
        _spawnPositions = new GameObject[4];
        StandardCoinAmount = 100;
        for (int i = 0; i < 4; ++i)
        {
            _spawnPositions[i] = _positions.transform.GetChild(i).gameObject;
        }
        IsShowMenu = false;
    }

    private void Update()
    {
        // ���� ���� �� �����ص� �� ����.
        // �׽�Ʈ�� �ڵ�
        _bearPriceUI.text = $"Buy\nBear\n{BearPrice}G";
        _foodPriceUI.text = $"Buy\nFood\n{_foodPrice}G";
    }

    /// <summary>
    /// ���� ����� �� ����Ǵ� �Լ�
    /// </summary>
    public void GetCoin()
    {
        RemainCoin += GetCoinAmount;
        _goldUI.text = $"Gold : {RemainCoin}G";
    }

    /// <summary>
    /// ������ 0�϶� ���� ���� ����
    /// </summary>
    public void StopSpawnCoin()
    {
        StopCoin.Invoke();
    }

    /// <summary>
    /// 0���� ȸ���Ǿ����� �ٽ� ���� ���� ����
    /// </summary>
    public void ContinueCoinSpawn()
    {
        RestartCoin.Invoke();
    }

    /// <summary>
    /// �̺�ƮŸ�ӿ��� ���� ����
    /// </summary>
    public void ChangeStatus()
    {
        IsEventTime = !IsEventTime;
    }

    /// <summary>
    /// �ǹ�Ÿ�� ����
    /// </summary>
    public void StartEventTime()
    {
        EventTime.Invoke();
    }

    /// <summary>
    /// �� �춧 ����Ǵ� �Լ�
    /// </summary>
    public void BuyNewBear()
    {
        if (RemainCoin - BearPrice < 0 || BearCount == 20)
        {
            return;
        }
        else
        {
            int num = Random.Range(0, 4);
            BearGroup.BirthBear(_spawnPositions[num].transform.position);

            RemainCoin -= BearPrice;
            _goldUI.text = $"Gold : {RemainCoin}G";
            _foodPrice = BearCount * 5;

        }
    }

    /// <summary>
    /// ���� �춧 ����Ǵ� �Լ�
    /// </summary>
    public void BuyNewFood()
    {
        if (RemainCoin - _foodPrice < 0 || _gagueManager._hungryGague == 100)
        {
            return;
        }
        else 
        {
            RemainCoin -= _foodPrice;
            _goldUI.text = $"Gold : {RemainCoin}G";
            BuyFood.Invoke();
        }
    }

    /// <summary>
    /// ȭ���� �̺�Ʈ
    /// </summary>
    public void AngryBearStatus()
    {
        AngryBear.Invoke();
    }

    /// <summary>
    /// �þ ���� ���� ���󺹱�
    /// </summary>
    public void RollbackBearStatus()
    {
        NormalBear.Invoke();
    }
}
