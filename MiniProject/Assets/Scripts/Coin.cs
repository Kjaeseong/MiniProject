using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private bool TimeOut;
    [SerializeField][Range(0, 5)] private float _moveSpeed;
    [SerializeField]private float CountTime = 5f;
    private float CountTimeDefault;

    private void Awake() 
    {
        CountTimeDefault = CountTime;
    }

    private void OnEnable() 
    {
        transform.position = transform.parent.position;
        CountTime = CountTimeDefault;
    }

    private void OnDisable() 
    {
        if(!TimeOut)
        {
            GameManager.Instance.Money += 5;
        }
    }

    private void Update() 
    {
        MoveCoin();
        CountingTime();
    }

    private void MoveCoin()
    {
        transform.position = new Vector2(0f, transform.position.y - _moveSpeed * Time.deltaTime);
    }

    private void CountingTime()
    {
        if(CountTime <= 0)
        {
            TimeOut = true;

            gameObject.SetActive(false);
        }
        else
        {
            CountTime -= Time.deltaTime;
        }
    }



}
