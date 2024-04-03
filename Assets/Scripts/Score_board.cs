using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score_board : MonoBehaviour
{
    public Text game_over;
    public Text points;
    public Text totem;
    public Text bonus;
    public Text total_points;

    public int tile_mnoznik = 500;
    public int time_mnoznik = 100;

    public Button play_again;
    public Button quit;

    void Start()
    {
        if(General_Play.finished == false)
        {
            game_over.color = Color.white;
            game_over.text = "Game Over";
        }
        else
        {
            game_over.text = "You Win!";
        }
        int totem_points = 0;
        int time_bonus = 0;
        if(General_Play.finished == true)
        {
            totem_points = MapCreation.ret_num_tiles() * tile_mnoznik;
        }
        if(TimerScript.Time_till_end > 0)
        {
            time_bonus = (int)TimerScript.Time_till_end * time_mnoznik;
        }

        points.text = (General_Play.points).ToString();
        totem.text = (totem_points).ToString();
        bonus.text = (time_bonus).ToString();

        total_points.text = (General_Play.points + totem_points + time_bonus).ToString();
    }
    public void quit_to_menu()
    {
        General_Play.reset_game();
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }
    public void play_again_action()
    {
        General_Play.reset_game();
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }
}
