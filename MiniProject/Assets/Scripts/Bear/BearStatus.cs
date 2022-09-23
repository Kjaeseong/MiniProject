using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// y: 2.8 ~ -4 x : -4.6 ~ 9.6
public class BearStatus : MonoBehaviour
{
    private GameObject _coinPosition;
    private Rigidbody2D _rigid;
    private float _moveX;
    private float _moveY;
    private float _elapsedTime;

    public float MoveDelayTime = 1f;
    public float MoveAmount = 0.1f;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _coinPosition = transform.GetChild(0).gameObject;

        StartCoroutine(SpawnCoin());
        StartCoroutine(BearMove());
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime < 1f)
        {
            MoveBear();
        }
        else if (_elapsedTime < 2f)
        {
            return;
        }
        else
        {
            _elapsedTime = 0.0f;
        }
        
    }

    private void MoveBear()
    {
        Vector2 _horizontalPosition = MoveAmount * Time.deltaTime * _moveX * transform.right;
        Vector2 _verticalPosition = MoveAmount * Time.deltaTime * _moveY * transform.up;
        Vector2 _newPosition = _rigid.position + _horizontalPosition + _verticalPosition;

        if (_newPosition.x > -4.6f && _newPosition.x < 9.6f && _newPosition.y > -4.0f && _newPosition.y < 2.8f)
        {
            _rigid.MovePosition(_newPosition);
        }
        else
        {
            if (_horizontalPosition.x < -4.6f)
            {
                _horizontalPosition.x = -4.6f;
            }
            if (_horizontalPosition.x > 9.6f)
            {
                _horizontalPosition.x = 9.6f;
            }
            if (_verticalPosition.y < -4.0f)
            {
                _verticalPosition.y = -4.0f;
            }
            if (_verticalPosition.y > 2.8f)
            {
                _verticalPosition.y = 2.8f;
            }
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
        }
    }

    IEnumerator BearMove()
    {
        while(true)
        {
            _moveX = Random.Range(-1, 2);
            _moveY = Random.Range(-1, 2);
            yield return new WaitForSeconds(MoveDelayTime);
        }
    }
}
