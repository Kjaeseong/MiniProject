using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingletonBehaviour<SpawnManager>
{
    [SerializeField]
    private GameObject Coin;
    [SerializeField]
    private GameObject CoinPoolingPosition;

    public GameObject[] CoinGroup;


    private void Start()
    {
        CoinGroup = new GameObject[100];

        for (int i = 0; i < 100; ++i)
        {
            GameObject coin = Instantiate(Coin);
            coin.transform.SetParent(CoinPoolingPosition.transform);
            coin.transform.position = CoinPoolingPosition.transform.position;
            CoinGroup[i] = coin;
            coin.SetActive(false);
        }
    }
}
