using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack_Script : MonoBehaviour
{
    public AudioSource src;
    public AudioClip soundtrack;
    void Start()
    {
        src.clip = soundtrack;
        src.Play();
    }
}
