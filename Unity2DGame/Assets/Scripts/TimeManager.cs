using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;

public class TimeManager : MonoBehaviour
{
    private double ct;
    public Text text_Time;

    private void Start()
    {
        ct = 0.0f;
    }

    private void Update()
    {
        ct += Time.deltaTime;
        text_Time.text = ((Math.Truncate(ct * 100)) / 100).ToString();
        if ((ControllerManager.GetInstance().Player_HP <= 0) || (ControllerManager.GetInstance().Boss_HP <= 0))
        {
            ControllerManager.GetInstance().CountTime = (Math.Truncate(ct * 100)) / 100;
            print(ControllerManager.GetInstance().CountTime);
        }
    }
}
