using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liany_koniec : MonoBehaviour
{
    public static int Index;

    public static void set_Index(int i)
    {
        Index = i;
    }

    public void ustaw_tile_liany()
    {
        
        GameObject[] gos;
        GameObject target;
        GameObject most;
        GameObject col;
        int z = 0;
        gos = GameObject.FindGameObjectsWithTag("Liany");
        for(int j = 0; j < gos.Length; j++)
        {
            z = gos[j].GetComponent<TileScript>().ret_index();
            if (z == Index)
            {
                target = gos[j];
                most = target.transform.Find("Most").gameObject;
                col = target.transform.Find("Game_collider").gameObject;
                most.SetActive(true);
                col.SetActive(false);
                General_Play.points += 1000;
                TimerScript.Time_till_end += 20;
                break;
            }
        }
    }
    
}
