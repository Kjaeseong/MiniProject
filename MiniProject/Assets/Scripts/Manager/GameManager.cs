using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehaviour<GameManager>
{
    public UnityEvent Spawn = new UnityEvent();
    public UnityEvent CoinGet = new UnityEvent();
    public UnityEvent BuyBear = new UnityEvent();

    [SerializeField]
    private GameObject Bear;

    [SerializeField]
    private GameObject[] SpawnPositions;

    [SerializeField]
    private TextMeshProUGUI _goldUI;
    [SerializeField]
    private TextMeshProUGUI _bearPrice;

    public int RemainCoin { get; private set; }

    public int GetCoinAmount = 5;
    public int BearPrice = 1000;


    public int BearCount { get; private set; }

    private void Start()
    {
        BearCount = 1;
    }

    private void Update()
    {
        _bearPrice.text = $"Buy\nBear\n{BearPrice}G";
    }

    public void GetCoin()
    {
        RemainCoin += GetCoinAmount;
        _goldUI.text = $"Gold : {RemainCoin}G";
    }

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
            Debug.Log(num);
            GameObject SpawnBear = Instantiate(Bear);

            SpawnBear.transform.position = SpawnPositions[num].transform.position;
            Spawn.Invoke();
            BuyBear.Invoke();
        }
    }

}
