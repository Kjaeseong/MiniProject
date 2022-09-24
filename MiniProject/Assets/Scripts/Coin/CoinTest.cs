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


    private void OnEnable()
    {
        StartCoroutine(Remain());

        // 코인 활성화 시 Rigidbody 중력 On, 위 방향으로 AddForce
        Rigid.simulated = true;
        Rigid.AddForce(transform.up * PushForce);
    }

    private void Update()
    {
        Spinning();
        Moving();

        _touchPosition = new Vector2(100f, 100f);

        // PC용 코드
        if (Input.GetMouseButton(0))
        {
            _touchPosition = Input.mousePosition;
        }

        // 모바일용 코드
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
            Debug.Log("터치 감지");
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
    /// 생성 시점의 케릭터 y좌표에 닿을때까지 중력 적용
    /// </summary>
    private void Moving()
    {
        if(transform.position.y <= StopPosition)
        {
            Rigid.simulated = false;
        }
    }
}
