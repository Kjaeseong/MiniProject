using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BearStatus : MonoBehaviour
{
    private GameObject _coinPosition;
    private TextMeshProUGUI _ui;
    private GameObject _textUI;
    private float startTime;
    private int DecreaseGagueCount;

    public int HungryGague { get; private set; }

    public int RemainCoin { get; private set; }

    Vector3 ui_position = new Vector3(0f, 0.5f, 0f);

    private void OnEnable()
    {
        GameManager.Instance.Spawn.AddListener(ChangeCount);
        GameManager.Instance.CoinGet.AddListener(IncreseCoin);
        GameManager.Instance.BuyBear.AddListener(Buy);
    }

    private void Buy()
    {
        RemainCoin -= 100;
    }

    private void IncreseCoin()
    {
        RemainCoin += 10;
    }

    private void ChangeCount()
    {
        DecreaseGagueCount = GameManager.Instance.BearCount;
    }

    private void Start()
    {
        startTime = Time.time;
        _ui = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        _textUI = _ui.gameObject;
        HungryGague = 100;
        RemainCoin = 0;
        _coinPosition = transform.GetChild(0).gameObject;

        DecreaseGagueCount = GameManager.Instance.BearCount;

        StartCoroutine(SpawnCoin());
        StartCoroutine(DecreaseHungryGague());
    }

    private void Update()
    {
        _textUI.transform.position = gameObject.transform.position + ui_position;
    }
    private void OnDisable()
    {
        GameManager.Instance.Spawn.RemoveListener(ChangeCount);
        GameManager.Instance.CoinGet.RemoveListener(IncreseCoin);
        GameManager.Instance.BuyBear.RemoveListener(Buy);
    }
    IEnumerator DecreaseHungryGague()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f);
            HungryGague -= DecreaseGagueCount;
            Debug.Log(DecreaseGagueCount);
            _ui.text = HungryGague.ToString();
        }
    }

    IEnumerator SpawnCoin()
    {
        while(true)
        {
            yield return new WaitForSeconds(10f);
            for (int i = 0; i < 100; ++i)
            {
                if (false == SpawnManager.Instance.CoinGroup[i].activeSelf)
                {
                    SpawnManager.Instance.CoinGroup[i].transform.position = _coinPosition.transform.position;
                    SpawnManager.Instance.CoinGroup[i].SetActive(true);
                    break;
                }
            }
            Debug.Log(Time.time - startTime);
        }
    }
}
