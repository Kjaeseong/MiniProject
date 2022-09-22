using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool A;


    private void Update() 
    {
        if(Input.GetKey(KeyCode.A))
        {
            A = true;
        }
        else
        {
            A = false;
        }
    }


}
