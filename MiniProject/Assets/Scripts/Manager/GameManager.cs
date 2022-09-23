using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehaviour<GameManager>
{
    public UnityEvent BuyFood = new UnityEvent();

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

    public int RemainCoin { get; private set; }

    public int GetCoinAmount = 5;
    public int BearPrice = 1000;
    public int FoodPrice = 100;

    public int BearCount { get; private set; }

    private void Start()
    {
        BearCount = 1;
    }

    private void Update()
    {
        // ���� ���� �� �����ص� �� ����.
        // �׽�Ʈ�� �ڵ�
        _bearPriceUI.text = $"Buy\nBear\n{BearPrice}G";
        _foodPriceUI.text = $"Buy\nFood\n{FoodPrice}G";
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
    /// �� �춧 ����Ǵ� �Լ�
    /// </summary>
    public void BuyNewBear()
    {
        if(RemainCoin - BearPrice < 0)
        {
            return;
        }
        else
        {
            RemainCoin -= BearPrice;
            _goldUI.text = $"Gold : {RemainCoin}G";
            ++BearCount;
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
        if (RemainCoin - FoodPrice < 0 || _gagueManager._hungryGague == 100)
        {
            return;
        }
        else 
        {
            RemainCoin -= FoodPrice;
            _goldUI.text = $"Gold : {RemainCoin}G";
            BuyFood.Invoke();
        }
    }

}
