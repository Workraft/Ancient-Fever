using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static float Time_till_end = 30;
    public Text txt;
    private int world_time = 1;
    public GameObject podpowiedz;
    float czas_podpowiedzi = 6;
    bool zanik_podp = false;
    public static float multiplier = 1.0f;


    void Start()
    {
        txt.text = Time_till_end.ToString();
    }

    void Update()
    {
        if(Time_till_end > 0)
        {
            if(czas_podpowiedzi > 0)
            {
                czas_podpowiedzi -= Time.deltaTime;
                if(czas_podpowiedzi < 0 && zanik_podp == false)
                {
                    zanik_podp = true;
                    podpowiedz.SetActive(false);    
                }
            }
            Time_till_end -= multiplier * Time.deltaTime;
            world_time = (int)Time_till_end;
            txt.text = world_time.ToString();
        }
        else
        {
            if(General_Play.game_ended == false)
            {
                //Debug.Log("Koniec Czasu");
                General_Play.koniec_gry();
            }
        }
    }
    static public void set_timer()
    {
        multiplier = 1.0f;
        Time_till_end = 30;
    }
}
