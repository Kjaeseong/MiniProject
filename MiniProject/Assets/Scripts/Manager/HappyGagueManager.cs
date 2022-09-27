using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyGagueManager : MonoBehaviour
{
    public enum State
    {
        DEFAULT,
        BEAR_BYE,
        ANGRY_BEAR,
        FEVER_TIME,
        CHANGE_BACKGROUND
    };

    public int EventStep { get; set; }

    void Update()
    {
        switch (EventStep)
        {
            case (int)State.BEAR_BYE:
                StartCoroutine(BearGoodBye());
                break;
            case (int)State.ANGRY_BEAR:
                break;
            case (int)State.FEVER_TIME:
                break;
            case (int)State.CHANGE_BACKGROUND:
                break;
            case (int)State.DEFAULT:
                break;
        }

    }

    IEnumerator BearGoodBye()
    {
        yield return new WaitForSeconds(20f);

    }
}
