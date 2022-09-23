using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// y: 3.7 ~ -4 x : -5.6 ~ 9.6
public class BearStatus : MonoBehaviour
{
    private GameObject _coinPosition;
    private Rigidbody2D _rigid;


    public float MoveDelayTime = 1f;
    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _coinPosition = transform.GetChild(0).gameObject;

        StartCoroutine(SpawnCoin());
        StartCoroutine(BearMove());
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

    IEnumerator BearMove()
    {
        while(true)
        {
            yield return new WaitForSeconds(MoveDelayTime);
            int num = Random.Range(1, 5);
            switch (num)
            {
                case 1: // 위로 이동
                    if (transform.position.y * 10f * Time.deltaTime <= 3.7)
                    {
                        Vector2 moveUp = new Vector2(transform.position.x, transform.position.y * 10f * Time.deltaTime);
                    }
                    break;
                case 2: // 아래로 이동

                    break;
                case 3: // 왼쪽으로 이동

                    break;
                case 4: // 오른쪽으로 이동

                    break;
            }

        }
    }
}
