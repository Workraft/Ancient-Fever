using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_minigame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Go_Back_Scene>().goBack();
        }
    }
}
