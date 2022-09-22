using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//-2.5~3.5
//-1.8~1.8
public class GameManager : SingletonBehaviour<GameManager>
{
    public UnityEvent Spawn = new UnityEvent();
    public UnityEvent CoinGet = new UnityEvent();
    public UnityEvent BuyBear = new UnityEvent();

    [SerializeField]
    private GameObject Bear;

    public int BearCount { get; private set; }

    private void Start()
    {
        BearCount = 1;
    }

    private void Update()
    {
    }
    public void GetCoin()
    {
        CoinGet.Invoke();
    }

    public void BuyNewBear()
    {
        ++BearCount;
        float y = Random.Range(-2.5f, 3.5f);
        float x = Random.Range(-1.8f, 1.8f);
        GameObject SpawnBear = Instantiate(Bear);

        SpawnBear.transform.position = new Vector3(x, y, 0f);
        Spawn.Invoke();
        BuyBear.Invoke();
    }

}
