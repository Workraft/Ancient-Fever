using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button Button;
    private Animator anim;
    public static bool anim_play = false;
    public void ButtonPressed()
    {}
    void Start()
    {
        Button.onClick.AddListener(ButtonPressed);
        anim = GetComponent<Animator>();
    }

    public void play_anim(bool graj)
    {
        if(graj == true)
        {
            anim.SetTrigger("OnTrig_Liany");
        }
        else
        {
            anim.SetTrigger("OnTrig_Left");
        }
    }
}
