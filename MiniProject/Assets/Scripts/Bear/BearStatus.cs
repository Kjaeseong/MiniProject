using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BearStatus : MonoBehaviour
{
    private GameObject _coinPosition;

    private void Start()
    {
        _coinPosition = transform.GetChild(0).gameObject;

        StartCoroutine(SpawnCoin());
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
        }
    }
}
