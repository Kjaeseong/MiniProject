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

    public int RemainCoin { get; private set; }

    public int GetCoinAmount = 5;
    public int StandardCoinAmount = 100;
    public int BearPrice = 1000;
    private int _foodPrice;

    public bool IsShowMenu { get; private set; }

    public int BearCount { get; private set; }

    public bool IsEventTime { get; private set; }

    private void Start()
    {
        IsEventTime = false;
        BearCount = 1;
        _foodPrice = BearCount * 10;
        _spawnPositions = new GameObject[4];
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

    public void ChangeStatus()
    {
        IsEventTime = !IsEventTime;
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
    /// Ÿ��Ʋ������ ���ư���
    /// </summary>
    public void GoingToTitleScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
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
    /// �޴� �ݱ�
    /// </summary>
    public void Menu()
    {
        IsShowMenu = !IsShowMenu;
        if(IsShowMenu == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        _pauseMenu.SetActive(IsShowMenu);
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
            RemainCoin -= BearPrice;
            _goldUI.text = $"Gold : {RemainCoin}G";
            ++BearCount;
            _foodPrice = BearCount * 10;
            int num = Random.Range(0, 4);
            GameObject SpawnBear = Instantiate(_bear);

            SpawnBear.transform.position = _spawnPositions[num].transform.position;
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
    /// �̺�Ʈ Ÿ�Ӷ� ȣ��� ����������
    /// </summary>
    public void StartEventTime()
    {
        EventTime.Invoke();
    }
}
