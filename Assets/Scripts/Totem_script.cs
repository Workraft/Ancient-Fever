using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem_script : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            General_Play.finished = true;
            General_Play.koniec_gry();
        }
    }
}
