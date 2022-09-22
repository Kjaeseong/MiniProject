using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Coin;
    [SerializeField]
    private GameObject CoinGroup;

    private void Start()
    {
        for (int i = 0; i < 100; ++i)
        {
            GameObject coin = Instantiate(Coin);
            coin.transform.SetParent(CoinGroup.transform);
            coin.transform.position = CoinGroup.transform.position;
            coin.SetActive(false);
        }
    }
}
