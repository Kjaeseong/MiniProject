using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GagueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _hungrygagueUI;
    [SerializeField]
    private Image _gagueBar;

    private int _hungryGague;

    public int RestoreAmount = 1;

    private void OnEnable()
    {
        GameManager.Instance.BuyFood.AddListener(RestoreHungryGague);
    }

    private void RestoreHungryGague()
    {
        _hungryGague += RestoreAmount;
        _hungrygagueUI.text = $"Hungry : {_hungryGague}";
    }

    private void Start()
    {
        _hungryGague = 100;
        StartCoroutine(HungryGague());
    }

    IEnumerator HungryGague()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f);
            _hungryGague -= GameManager.Instance.BearCount;
            _hungrygagueUI.text = $"Hungry : {_hungryGague}";
            _gagueBar.fillAmount = (float)_hungryGague / 100;
        }
    }
}
