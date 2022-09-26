using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 120f;
    private Vector2 _touchPosition;
    private Rigidbody2D _rigid;

    // �׽�Ʈ ��� 150 ����.
    [SerializeField] [Range(0, 1000)] private float PushForce;
    public float StopPosition;
    private Vector3 FixPosition;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // ���� Ȱ��ȭ �� �ڷ�ƾ ����, �߷� On, �� �������� AddForce
        StartCoroutine(Remain());

        _rigid.constraints = RigidbodyConstraints2D.None;
        _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigid.AddForce(transform.up * PushForce);
        _rigid.AddForce(transform.right * PushForce / 2);
    }

    private void Update()
    {
        Spinning();
        CollisionWithTheWall();

        // ���� ��ġ�� Ȱ��ȭ�� �޾ƿ� y�� ��ǥ�� �������� �׸� ������
        if (transform.position.y <= StopPosition)
        {
            StopFalling();
        }

        _touchPosition = new Vector2(100f, 100f);

        // PC ���콺 Ŭ��
        if (Input.GetMouseButton(0))
        {
            _touchPosition = Input.mousePosition;
        }

        // ����ϵ���̽� ��ġ
        if (Input.touchCount > 0)
        {
            _touchPosition = Input.GetTouch(0).position;
        }

        // Ŭ��(��ġ)��ġ���� ����ĳ��Ʈ
        Vector2 postion = Camera.main.ScreenToWorldPoint(_touchPosition);
        Ray2D ray = new Ray2D(postion, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            GameManager.Instance.GetCoin();
            StopCoroutine(Remain());
            hit.collider.gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// ������ ���������� ������� ������ ���ڸ� ȸ��
    /// </summary>
    private void Spinning()
    {
        transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
    }

    /// <summary>
    /// ������ 5�� �ڿ� �������.
    /// </summary>
    IEnumerator Remain()
    {
        yield return new WaitForSeconds(5f);
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �������°� ����
    /// </summary>
    private void StopFalling()
    {
        _rigid.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    /// <summary>
    /// ������ ���� ������ �ݴ� �������� ƨ���� ����
    /// </summary>
    private void CollisionWithTheWall()
    {
        if (transform.position.y > 9.5f)
        {
            _rigid.AddForce(transform.right * -1f * PushForce / 4);
        }
    }

}
