using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearGroup : MonoBehaviour
{
    public GameObject CuteBear;
    public int BearGroupSize = 20;
    
    private GameObject[] _bearPool;
    private int _bearNum;


    void Start()
    {
        _bearNum = 0;
        CreateBearPool(BearGroupSize);
        
        Vector3 StartPosition = new Vector3(0f, 0f, 0f);
        BirthBear(StartPosition);
    }

    private void CreateBearPool(int Size)
    {
        _bearPool = new GameObject[BearGroupSize];

        for(int i = 0; i < BearGroupSize; i++)
        {
            _bearPool[i] = Instantiate(CuteBear);
            _bearPool[i].name = "CuteBear_" + (i + 1);
            _bearPool[i].transform.parent = gameObject.transform;
            _bearPool[i].SetActive(false);
        }
    }

    public void BirthBear(Vector3 SpawnPosition)
    {
        for(int i = 0; i < BearGroupSize; i++)
        {
            if(_bearPool[i].activeSelf == false)
            {
                _bearPool[i].transform.position = SpawnPosition;
                _bearPool[i].SetActive(true);
                break;
            }
        }
        ++_bearNum;
        GameManager.Instance.BearCount = _bearNum;
    }

    public void DeleteBear()
    {
        for(int i = 0; i < BearGroupSize; i++)
        {
            if(_bearPool[i].activeSelf == false && _bearPool.Length > 0)
            {
                _bearPool[i - 1].SetActive(false);
            }
        }
        --_bearNum;
        GameManager.Instance.BearCount = _bearNum;
    }
}
