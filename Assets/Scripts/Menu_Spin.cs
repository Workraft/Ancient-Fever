using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Spin : MonoBehaviour
{
    public Image circe_1;
    public Image circe_2;
    float degreesPerSecond = 10;
    void Update()
    {
        circe_1.transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
        circe_2.transform.Rotate(new Vector3(0, 0, degreesPerSecond) * -Time.deltaTime);
    }
}
