using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndUI : MonoBehaviour
{
    private void OnEnable() 
    {
        Time.timeScale = 0f;
        Debug.Log("");
    }

    private void OnDisable() 
    {
        Time.timeScale = 1f;
    }
}
