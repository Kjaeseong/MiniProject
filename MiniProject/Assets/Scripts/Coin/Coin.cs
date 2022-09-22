using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Vector2 _touchPosition;
    void Start()
    {
        StartCoroutine(Remain());
    }

    void Update()
    {
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
            //touch event;
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator Remain()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
