using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond_Script : MonoBehaviour
{
    public int diamond_value = 100;
    public float add_time = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            General_Play.points += diamond_value;
            TimerScript.Time_till_end += add_time;
            Destroy(gameObject);
        }        
    }
}
