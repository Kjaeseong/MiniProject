using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTest : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 120f;
    private Vector2 _touchPosition;
    public Rigidbody2D Rigid;

    // �׽�Ʈ ��� 150 ����.
    [SerializeField][Range(0, 1000)] private float PushForce;
    public float StopPosition;
    private Vector3 FixPosition;
    
    private int[] _coinDirection = {-1, 1};

    private void OnEnable()
    {
        // ���� Ȱ��ȭ �� �ڷ�ƾ ����, �߷� On, �� �������� AddForce
        StartCoroutine(Remain());

        Rigid.constraints = RigidbodyConstraints2D.None;
        Rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        Rigid.AddForce(transform.up * PushForce);
        Rigid.AddForce(transform.right * (_coinDirection[Random.Range(0, 2)] * PushForce / 2));
    }

    private void Update()
    {
        Spinning();
        CollisionWithTheWall();

        // ���� ��ġ�� Ȱ��ȭ�� �޾ƿ� y�� ��ǥ�� �������� �׸� ������
        if(transform.position.y <= StopPosition)
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
        Rigid.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    /// <summary>
    /// ������ ���� ������ �ݴ� �������� ƨ���� ����
    /// </summary>
    private void CollisionWithTheWall()
    {
        if(transform.position.x < -5.5f)
        {
            Rigid.AddForce(transform.right * (_coinDirection[1] * PushForce / 4));
        }
        if(transform.position.y > 9.5f)
        {
            Rigid.AddForce(transform.right * (_coinDirection[0] * PushForce / 4));
        }
    }
    
}
