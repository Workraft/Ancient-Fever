using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_walk_sound : MonoBehaviour
{
    public AudioSource src;
    public AudioClip walk_sound;
    public Joystick joy;
    bool is_playing = false;

    private void Start()
    {
        src.clip = walk_sound;
    }
    private void Update()
    {
        if(joy.Horizontal != 0 || joy.Vertical != 0)
        {
            if( is_playing == false)
            {
                src.clip = walk_sound;
                src.Play();
                is_playing = true;
            }
        }
        else
        {
            if(is_playing == true)
            {
                src.Stop();
                is_playing = false;
            }
        }
    }
}
