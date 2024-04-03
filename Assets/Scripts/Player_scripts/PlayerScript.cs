using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float MovementSpeed;
    public Joystick joystick;
    float horizontalMove = 0.0f;
    float verticalMove = 0.0f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void play_anim()
    {
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if(joystick.Horizontal < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            anim.SetTrigger("Is_Walking");
        }
        else
        {
            anim.SetTrigger("Stoped_Walking");
        }
    }

    void Update()
    {
        if(General_Play.game_ended == false)
        {
            play_anim();
            horizontalMove = joystick.Horizontal * MovementSpeed * Time.deltaTime;
            verticalMove = joystick.Vertical * MovementSpeed * Time.deltaTime;

            transform.Translate(Vector3.forward * verticalMove);
            transform.Translate(Vector3.right * horizontalMove);
        }
    }

    public float get_horizontalMove()
    {
        return horizontalMove;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collider_Liany")
        {
            GameObject parent = other.transform.parent.gameObject;
            int index = parent.GetComponentInParent<TileScript>().ret_index();
            Debug.LogError(index);
            Liany_koniec.Index = index;
        }
    }
}
