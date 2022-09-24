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


    private void OnEnable()
    {
        StartCoroutine(Remain());

        // ���� Ȱ��ȭ �� Rigidbody �߷� On, �� �������� AddForce
        Rigid.simulated = true;
        Rigid.AddForce(transform.up * PushForce);
    }

    private void Update()
    {
        Spinning();
        Moving();

        _touchPosition = new Vector2(100f, 100f);

        // PC�� �ڵ�
        if (Input.GetMouseButton(0))
        {
            _touchPosition = Input.mousePosition;
        }

        // ����Ͽ� �ڵ�
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
            Debug.Log("��ġ ����");
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
    /// ���� ������ �ɸ��� y��ǥ�� ���������� �߷� ����
    /// </summary>
    private void Moving()
    {
        if(transform.position.y <= StopPosition)
        {
            Rigid.simulated = false;
        }
    }
}
