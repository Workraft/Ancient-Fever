using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class General_Play : MonoBehaviour
{
    public static int points = 0;
    public static bool finished = false;
    public static bool game_ended = false;
    public static void reset_game()
    {
        points = 0;
        game_ended = false;
        finished = false;
        TimerScript.set_timer();    
    }

    public static void koniec_gry()
    {
        game_ended = true;
        GameObject.FindGameObjectWithTag("Only_canvas").SetActive(false);
        SceneManager.LoadSceneAsync("Game_over", LoadSceneMode.Additive);
    }
}
