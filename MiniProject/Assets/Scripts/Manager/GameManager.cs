using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehaviour<GameManager>
{
    public UnityEvent BuyFood = new UnityEvent();

    [SerializeField] //곰 오브젝트
    private GameObject _bear;
    [SerializeField] // 곰 스폰 위치
    private GameObject[] _spawnPositions;
    [SerializeField] // Gold UI
    private TextMeshProUGUI _goldUI;
    [SerializeField] // 곰 가격 UI
    private TextMeshProUGUI _bearPriceUI;
    [SerializeField] // 먹이 가격 UI
    private TextMeshProUGUI _foodPriceUI;
    [SerializeField] // 게이지 매니저
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
        // 추후 빌드 때 삭제해도 될 내용.
        // 테스트용 코드
        _bearPriceUI.text = $"Buy\nBear\n{BearPrice}G";
        _foodPriceUI.text = $"Buy\nFood\n{FoodPrice}G";
    }

    /// <summary>
    /// 코인 얻었을 때 실행되는 함수
    /// </summary>
    public void GetCoin()
    {
        RemainCoin += GetCoinAmount;
        _goldUI.text = $"Gold : {RemainCoin}G";
    }

    /// <summary>
    /// 곰 살때 실행되는 함수
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
    /// 먹이 살때 실행되는 함수
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
