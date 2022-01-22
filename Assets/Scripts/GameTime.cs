using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    private Text timeText;
    private string time;
    private float startTime;
    private float gameTime;
    private float hr;
    private float min;
    private float sec;
    public static string finalTime;

    void Start()
    {
        timeText = GetComponent<Text>();
    }

    void Update()
    {
        if (GameManagement.Life > 0 && GameManagement.Pellets > 0 && GameManagement.StartMovement == true) 
        {
            if (startTime == 0)
            {
                startTime = Time.time;
                Debug.Log("Game Start Time " + startTime);
            }
            gameTime = Time.time - startTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
            time = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            timeText.text = "Game Time " + time;
        }
        else
        {
            finalTime = time;
        }
    }

    public static string FinalTime
    {
        get { return finalTime; }
    }



}
