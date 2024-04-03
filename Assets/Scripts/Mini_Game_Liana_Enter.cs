using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mini_Game_Liana_Enter : MonoBehaviour
{
    public GameObject par;
    private GameObject action;
    public Button actionBut;
    private void ButtonPressed()
    {
        actionBut.GetComponent<ButtonScript>().play_anim(false);
        par.GetComponentInParent<Scene_menagement>().playGame("Liany_mini_game");
        actionBut.onClick.RemoveAllListeners();
        actionBut.transform.localScale = new Vector3(1.5f, 6f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            actionBut.onClick.AddListener(ButtonPressed);
            action.GetComponent<ButtonScript>().play_anim(true);
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            actionBut.onClick.RemoveAllListeners();
            action.GetComponent<ButtonScript>().play_anim(false) ;
        }
    }
    private void Start()
    {
        actionBut = GameObject.FindGameObjectWithTag("Main_action_button").GetComponent<Button>();
        action = GameObject.FindGameObjectWithTag("Main_action_button");
    }
}
