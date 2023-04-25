using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private Text txt;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            print("1");
        if (Input.GetKeyDown(KeyCode.Keypad2))
            print("2");
        if (Input.GetKeyDown(KeyCode.Keypad3))
            print("3");
        if (Input.GetKeyDown(KeyCode.Keypad0))
            print("0");
    }
}
