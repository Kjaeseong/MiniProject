using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTest : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 120f;
    private Vector2 _touchPosition;
    public Rigidbody2D Rigid;

    // 테스트 결과 150 권장.
    [SerializeField][Range(0, 1000)] private float PushForce;
    public float StopPosition;
    private Vector3 FixPosition;
    
    private int[] _coinDirection = {-1, 1};

    private void OnEnable()
    {
        // 코인 활성화 시 코루틴 시작, 중력 On, 위 방향으로 AddForce
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

        // 현재 위치가 활성화시 받아온 y축 좌표와 같아지면 그만 떨어짐
        if(transform.position.y <= StopPosition)
        {
            StopFalling();
        }

        _touchPosition = new Vector2(100f, 100f);

        // PC 마우스 클릭
        if (Input.GetMouseButton(0))
        {
            _touchPosition = Input.mousePosition;
        }

        // 모바일디바이스 터치
        if (Input.touchCount > 0)
        {
            _touchPosition = Input.GetTouch(0).position;
        }

        // 클릭(터치)위치에서 레이캐스트
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
    /// 코인이 등장했으면 사라지기 전까지 제자리 회전
    /// </summary>
    private void Spinning()
    {
        transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
    }

    /// <summary>
    /// 코인은 5초 뒤에 사라진다.
    /// </summary>
    IEnumerator Remain()
    {
        yield return new WaitForSeconds(5f);
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 떨어지는것 멈춤
    /// </summary>
    private void StopFalling()
    {
        Rigid.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    /// <summary>
    /// 동전이 벽에 닿으면 반대 방향으로 튕겨져 나감
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
