using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_New_Game_Menu : MonoBehaviour
{
    public Button start;
    public void ButtonPressed()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    void Start()
    {
        start.onClick.AddListener(ButtonPressed);
    }
}
