using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// y: 2.8 ~ -4 x : -4.6 ~ 9.6
public class BearStatusTest : MonoBehaviour
{
    private GameObject _coinPosition;
    public JS_GagueManagerTest _gague;
    private Rigidbody2D _rigid;
    private float _moveX;
    private float _moveY;
    private float _elapsedTime;
    private int _playCount;
    private int _index;
    private Animator _ani;
    [SerializeField]
    private AnimationClip[] _animations;

    public float MoveDelayTime = 1f;
    public float MoveAmount = 0.1f;

    private void Start()
    {
        _playCount = 0;
        _index = Random.Range(1, 13);
        _rigid = GetComponent<Rigidbody2D>();
        _coinPosition = transform.GetChild(0).gameObject;
        _ani = GetComponent<Animator>();
        _gague = GameObject.Find("GagueManager").GetComponent<JS_GagueManagerTest>();

        StartCoroutine(SpawnCoin());
        StartCoroutine(BearMove());
    }

    private void OnEnable()
    {
        GameManager.Instance.StopCoin.AddListener(StopCoinSpawn);
        GameManager.Instance.RestartCoin.AddListener(RestartCoinSpawn);
    }

    private void RestartCoinSpawn()
    {
        StartCoroutine(SpawnCoin());
    }

    private void StopCoinSpawn()
    {
        StopCoroutine(SpawnCoin());
    }

    private void Update()
    {
        // 1초 움직이고 1초 idle 
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime < 1f)
        {
            _ani.Play("Move", 0);
            MoveBear();
        }
        else if (_elapsedTime > 1f && _elapsedTime < 2f)
        {
            if (_index < 10)
            {
                _ani.Play($"Bear_IDLE0{_index}", 0);
            }
            else
            {
                _ani.Play($"Bear_IDLE{_index}", 0);

            }

            if(_playCount > 15)
            {
                _playCount = 0;
                _index = Random.Range(1, 13);
            }
            return;
        }
        else
        {
            _elapsedTime = 0.0f;
        }
        
    }

    /// <summary>
    /// 곰을 움직인다.
    /// </summary>
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

    /// <summary>
    /// 10초마다 곰에서 코인 생성
    /// </summary>
    IEnumerator SpawnCoin()
    {
        while(true)
        {
            yield return new WaitForSeconds(10f);
            for (int i = 0; i < 100; ++i)
            {
                if (false == SpawnManager.Instance.CoinGroup[i].activeSelf)
                {
                    // 배고픔 게이지 0초과일 때만 코인 생성
                    if(_gague._hungryGague > 0)
                    {
                        SpawnManager.Instance.CoinGroup[i].transform.position = _coinPosition.transform.position;
                        SpawnManager.Instance.CoinGroup[i].SetActive(true);

                        // 코인 중력효과
                        // 코인 오브젝트 활성화 시 Bear Y축 좌표 전달
                        CoinTest coinScript;
                        coinScript = SpawnManager.Instance.CoinGroup[i].GetComponent<CoinTest>();
                        coinScript.StopPosition = transform.position.y - 1;
                    }
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 1초마다 움직일 포지션 재정의
    /// </summary>
    IEnumerator BearMove()
    {
        while(true)
        {
            _moveX = Random.Range(-1, 2);
            _moveY = Random.Range(-1, 2);
            ++_playCount;
            yield return new WaitForSeconds(MoveDelayTime);
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.StopCoin.RemoveListener(StopCoinSpawn);
        GameManager.Instance.RestartCoin.RemoveListener(RestartCoinSpawn);
    }
}
