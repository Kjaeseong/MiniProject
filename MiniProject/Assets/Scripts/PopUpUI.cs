using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpUI : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public string EventDescription { get; private set; }

    public List<Dictionary<string, object>> TextCSV;

    private void Awake() 
    {
        TextCSV = CSVReader.Read ("PopUpText");
    }

    private void OnEnable() 
    {
        Time.timeScale = 0;

        PopUpText("HappyGagueUp");
    }

    private void OnDisable() 
    {
        Time.timeScale = 1;
    }

    public void PopUpText(string eventDescription)
    {
        EventDescription = eventDescription;

        for(int i = 0; i < TextCSV.Count; i++)
        {
            if(EventDescription == TextCSV[i]["eventDscription"].ToString())
            {
                Text.text = TextCSV[i]["Text"].ToString();
            }
        }
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
