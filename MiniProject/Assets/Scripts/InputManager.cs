using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool IsTouch;

    public GameObject _hitObject;

    void Update()
    {
        Touch();
    }

    private void Touch()
    {
        if(Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(Input.touchCount > 0 && Input.touchCount <= 1)
            {
                DetectedObject();
            }
        }
    }

    private void DetectedObject()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Ray2D ray = new Ray2D(pos, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if(hit.collider != null)
        {
            if(hit.collider.tag == "Coin")
            {
                hit.collider.gameObject.SetActive(false);
            }
            
        }
    }
}
