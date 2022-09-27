using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditWindow : MonoBehaviour
{

    public GameObject TitleWindow;

    private void FixedUpdate() 
    {
        if(Input.touchCount > 0)
        {
            DeActivate();
        }
    }

    private void DeActivate()
    {
        gameObject.SetActive(false);
        TitleWindow.SetActive(true);
    }
}
