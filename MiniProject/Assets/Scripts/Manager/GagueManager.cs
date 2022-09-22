using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GagueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _hungrygagueUI;
    [SerializeField]
    private Image _gagueBar;

    private int _hungryGague;

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
