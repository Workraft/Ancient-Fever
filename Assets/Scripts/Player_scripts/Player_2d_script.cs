using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_2d_script : MonoBehaviour
{

    public float MovementSpeed = 1.0f;
    public Joystick joystick;
    float horizontalMove = 0.0f;

    public Button jump_button;

    public Vector3 Velociy_wne_vector;
    Transform currentSwing;

    public Collider Col;

    bool swing = false;

    float timer = 0;
    float jump_timer = 0;

    private Animator anim;
    
    public void ButtonPressed()
    {
        if(swing == false)
        {
            if(jump_timer <= 0)
            {
                GetComponent<Rigidbody>().AddForce(0, 6.0f, 0, ForceMode.Impulse);
                GetComponent<Player_hit_water>().player_jump();
                jump_timer = 1.5f;
            }
            
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(currentSwing.GetComponent<Rigidbody>().velocity.x, currentSwing.GetComponent<Rigidbody>().velocity.y + 2, currentSwing.GetComponent<Rigidbody>().velocity.z);
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Player_hit_water>().player_jump();
            swing = false;
            timer = 0.5f;
        }
        
    }
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Water") == true)
        {
            transform.position = new Vector3(11.0f, -100.0f, 0.0f);
            GetComponent<Player_hit_water>().hit_water();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(timer <= 0)
        {
            if (other.tag == "Liana")
            {
                Col = other;
                other.GetComponent<Rigidbody>().velocity = Velociy_wne_vector;
                other.GetComponent<Rigidbody>().AddForce(-12.0f, 0, 0, ForceMode.Impulse);
                swing = true;
                currentSwing = other.transform;
            }
        }
    }

    void Start()
    {
        jump_button.onClick.AddListener(ButtonPressed);
        anim = GetComponent<Animator>();
    }

    public void play_anim()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if (joystick.Horizontal < 0)
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
        if (jump_timer > 0)
        {
            jump_timer -= Time.deltaTime;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        play_anim();
        horizontalMove = joystick.Horizontal * MovementSpeed * Time.deltaTime;
        if (swing == false)
        {
            transform.Translate(Vector3.right * horizontalMove);
        }
        else
        {
            Col.GetComponent<Rigidbody>().AddForce(horizontalMove, 0, 0, ForceMode.Force);
            transform.position = currentSwing.position;
        }
    }
}
