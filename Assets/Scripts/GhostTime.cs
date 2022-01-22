using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTime : MonoBehaviour
{
    private Text timeText;
    private float timeInt;
    void Start()
    {
        timeText = GetComponent<Text>();
        timeInt = GameManagement.ScaredTime;
        timeInt = 0;
    }

    void Update()
    {
        timeInt = GameManagement.ScaredTime;
        timeInt = (int)Math.Round(timeInt);
        timeText.text = "Ghost Scared Time : " + timeInt;
    }
}
