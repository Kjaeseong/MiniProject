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
    [SerializeField] // 일시정지메뉴
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
        // 추후 빌드 때 삭제해도 될 내용.
        // 테스트용 코드
        _bearPriceUI.text = $"Buy\nBear\n{BearPrice}G";
        _foodPriceUI.text = $"Buy\nFood\n{_foodPrice}G";
    }

    public void ChangeStatus()
    {
        IsEventTime = !IsEventTime;
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
    /// 타이틀씬으로 돌아가기
    /// </summary>
    public void GoingToTitleScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 포만감 0일때 코인 생성 멈춤
    /// </summary>
    public void StopSpawnCoin()
    {
        StopCoin.Invoke();
    }

    /// <summary>
    /// 0에서 회복되었을때 다시 코인 생성 시작
    /// </summary>
    public void ContinueCoinSpawn()
    {
        RestartCoin.Invoke();
    }

    /// <summary>
    /// 메뉴 닫기
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
    /// 곰 살때 실행되는 함수
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
    /// 먹이 살때 실행되는 함수
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
    /// 이벤트 타임때 호출될 옵저버패턴
    /// </summary>
    public void StartEventTime()
    {
        EventTime.Invoke();
    }
}
