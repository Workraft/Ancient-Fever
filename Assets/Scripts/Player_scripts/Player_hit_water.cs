using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_hit_water : MonoBehaviour
{
    public GameObject scr;
    public AudioClip water_hit;
    public AudioClip jump;

    public void hit_water()
    {
        scr.GetComponent<AudioSource>().clip = water_hit;
        scr.GetComponent<AudioSource>().Play();
    }
    public void player_jump()
    {
        scr.GetComponent<AudioSource>().clip = jump;
        scr.GetComponent<AudioSource>().Play();
    }
}
