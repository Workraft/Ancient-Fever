using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Go_Back_Scene : MonoBehaviour
{
    public void goBack()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Liany_mini_game"));
        Scene_menagement.loaded = false;
    }
}
