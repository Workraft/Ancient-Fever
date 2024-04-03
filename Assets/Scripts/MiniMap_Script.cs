using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap_Script : MonoBehaviour
{
    public GameObject minimap;
    bool shown = false;
    public void ButtonPressed()
    {
        if(shown == false)
        {
            minimap.SetActive(true);
            TimerScript.Time_till_end -= 5.0f;
            TimerScript.multiplier += 1.0f;
            shown = true;
        }
        else
        {
            minimap.SetActive(false);
            TimerScript.multiplier -= 1.0f;
            shown = false;
        }   
    }
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonPressed);
    }
}
