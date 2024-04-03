using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGame_Timer : MonoBehaviour
{
    public Text txt;
    private int world_time = 1;
    void Update()
    {
        if (TimerScript.Time_till_end > 0)
        {
            TimerScript.Time_till_end -= Time.deltaTime;
            world_time = (int)TimerScript.Time_till_end;
            txt.text = world_time.ToString();
        }
        else
        {
            //Debug.Log("Koniec Czasu");
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Liany_mini_game"));
            Scene_menagement.loaded = false;
        }
    }
}
