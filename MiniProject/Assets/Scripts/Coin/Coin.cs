using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 120f;
    private Vector2 _touchPosition;
    private void OnEnable()
    {
        StartCoroutine(Remain());
    }

    private void Update()
    {
        Spinning();
        _touchPosition = new Vector2(100f, 100f);

        if (Input.GetMouseButton(0))
        {
            _touchPosition = Input.mousePosition;
        }

        if(Input.touchCount > 0)
        {
            _touchPosition = Input.GetTouch(0).position;
        }

        Vector2 postion = Camera.main.ScreenToWorldPoint(_touchPosition);
        Ray2D ray = new Ray2D(postion, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            GameManager.Instance.GetCoin();
            StopCoroutine(Remain());
            this.gameObject.SetActive(false);
        }
    }

    private void Spinning()
    {
        transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
    }

    IEnumerator Remain()
    {
        yield return new WaitForSeconds(5f);
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}
