using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_menagement : MonoBehaviour
{
    public GameObject ourMain;
    public static bool loaded = false;
    bool check = false;
    public void playGame(string name)
    {
        ourMain.SetActive(false);
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive); 
        loaded = true;
        check = true;
    }

    private void Update()
    {
        if(loaded == false && check == true)
        {
            ourMain.SetActive(true);
            if(TimerScript.Time_till_end > 0)
            {
                GameObject.FindGameObjectWithTag("Spawn").GetComponent<Liany_koniec>().ustaw_tile_liany();
            }
            check = false;
        }
    }
    private void Start()
    {
        ourMain = GameObject.FindGameObjectWithTag("Cam_canv");
    }
}
