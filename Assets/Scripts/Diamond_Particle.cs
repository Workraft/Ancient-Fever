using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond_Particle : MonoBehaviour
{
    bool played = false;
    public AudioSource src;
    public AudioClip coin;

    private void OnTriggerEnter(Collider other)
    {
        if(played == false)
        {
            if (other.tag == "Player")
            {
                GetComponent<ParticleSystem>().Play();
                src.clip = coin;
                src.Play();
                played = true;
            }
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
