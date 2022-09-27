using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// y: 2.8 ~ -4 x : -4.6 ~ 9.6
public class BearStatus : MonoBehaviour
{
    private GameObject _coinPosition;
    public GagueManager _gague;
    private Rigidbody2D _rigid;
    private float _moveX;
    private float _moveY;
    private float _elapsedTime;
    private int _playCount;
    private int _index;
    private Animator _ani;
    private float _spawnCoinTime = 10f;
    private float _standardSpawnCoin = 10f;

    public float MoveDelayTime = 1f;
    public float MoveAmount = 0.1f;

    [SerializeField]
    private int _animationCount = 13;

    private bool _finishBirth;


    private void Start()
    {
        _finishBirth = false;
        _playCount = 0;
        _index = Random.Range(1, 14);
        _rigid = GetComponent<Rigidbody2D>();
        _coinPosition = transform.GetChild(0).gameObject;
        _ani = GetComponent<Animator>();
        _gague = GameObject.Find("GagueManager").GetComponent<GagueManager>();

        StartCoroutine(BirthBear());

    }

    private void OnEnable()
    {
        GameManager.Instance.StopCoin.AddListener(StopCoinSpawn);
        GameManager.Instance.RestartCoin.AddListener(RestartCoinSpawn);
        GameManager.Instance.EventTime.AddListener(FeverTime);
        GameManager.Instance.AngryBear.AddListener(ReleaseCoinTime);
        GameManager.Instance.NormalBear.AddListener(ResetCoinTime);
    }

    /// <summary>
    /// ���� �����ð� �ø�
    /// </summary>
    private void ReleaseCoinTime()
    {
        _spawnCoinTime = _standardSpawnCoin * 2;
    }

    /// <summary>
    /// ���� �����ð� ���󺹱�
    /// </summary>
    private void ResetCoinTime()
    {
        _spawnCoinTime = _standardSpawnCoin;
    }

    /// <summary>
    /// ���� ����� ����
    /// </summary>
    private void RestartCoinSpawn()
    {
        StartCoroutine(SpawnCoin());
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    private void StopCoinSpawn()
    {
        StopCoroutine(SpawnCoin());
    }

    private void Update()
    {
        if (_finishBirth == true)
        { // �ູ�������� ���� �̺�Ʈ Ÿ���̸� �̺�Ʈ Ÿ���� �ֿ켱 ����
            if (GameManager.Instance.IsEventTime == false)
            {
                // 2�� �����̰� 4�� IDLE
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime < 2f)
                {
                    MoveBear();
                }
                else if (_elapsedTime > 2f && _elapsedTime < 6f)
                {
                    if (false == _ani.GetBool($"isIDLE{_index}"))
                    {
                        _ani.SetBool($"isIDLE{_index}", true);
                    }
                    _ani.SetBool("canMove", false);
                }
                if (_elapsedTime >= 6f)
                {
                    _elapsedTime = 0.0f;
                }

                if (_playCount > 15)
                {
                    _playCount = 0;
                    _index = Random.Range(1, 14);
                }
            }

            else
            {
                return;
            }
        }
    }

    /// <summary>
    /// ���� �����δ�.
    /// </summary>
    private void MoveBear()
    {
        _ani.SetBool("canMove", true);

        for (int i = 1; i <= _animationCount; ++i)
        {
            _ani.SetBool($"isIDLE{i}", false);
        }

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
            if (_horizontalPosition.x > 8.6f)
            {
                _horizontalPosition.x = 8.6f;
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
    /// �̺�Ʈ Ÿ��
    /// </summary>
    private void FeverTime()
    {
        int _index = Random.Range(1, 6);
        _ani.SetBool("canMove", false);
        _ani.Play($"Bear_Dance0{_index}");
        _spawnCoinTime = _standardSpawnCoin / 2;
        Invoke("EndFeverTime", 30f);
    }
    private void EndFeverTime()
    {
        _spawnCoinTime = _standardSpawnCoin;
        _ani.SetBool("canMove", false);
    }

    /// <summary>
    /// ó�� ���� �ִϸ��̼� ���
    /// </summary>
    IEnumerator BirthBear()
    {
        yield return new WaitForSeconds(3f);
        _ani.SetBool("canMove", true);
        StartCoroutine(SpawnCoin());
        StartCoroutine(BearMove());
        _finishBirth = true;
    }

    /// <summary>
    /// 10�ʸ��� ������ ���� ����
    /// </summary>
    IEnumerator SpawnCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnCoinTime);
            for (int i = 0; i < 100; ++i)
            {
                if (false == SpawnManager.Instance.CoinGroup[i].activeSelf)
                {
                    // ����� ������ 0�ʰ��� ���� ���� ����
                    if (_gague._hungryGague > 0)
                    {
                        SpawnManager.Instance.CoinGroup[i].transform.position = _coinPosition.transform.position;
                        SpawnManager.Instance.CoinGroup[i].SetActive(true);

                        // ���� �߷�ȿ��
                        // ���� ������Ʈ Ȱ��ȭ �� Bear Y�� ��ǥ ����
                        Coin coinScript;
                        coinScript = SpawnManager.Instance.CoinGroup[i].GetComponent<Coin>();
                        coinScript.StopPosition = transform.position.y - 0.3f;
                    }
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 1�ʸ��� ������ ������ ������
    /// </summary>
    IEnumerator BearMove()
    {
        while (true)
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
        GameManager.Instance.EventTime.RemoveListener(FeverTime);
        GameManager.Instance.AngryBear.RemoveListener(ReleaseCoinTime);
    }
}
