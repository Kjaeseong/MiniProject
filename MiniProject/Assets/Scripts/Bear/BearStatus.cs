using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearStatus : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(HungryGague());
        StartCoroutine(SpawnCoin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HungryGague()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f);
        }
    }

    IEnumerator SpawnCoin()
    {
        while(true)
        {
            yield return new WaitForSeconds(5f);
        }
    }
}
