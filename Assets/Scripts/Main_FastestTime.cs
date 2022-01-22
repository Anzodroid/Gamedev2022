using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_FastestTime : MonoBehaviour
{
    private string hightime;
    private Text timeText;
    void Start()
    {
        timeText = GetComponent<Text>();
        hightime = PlayerPrefs.GetString("FastestTime", "00:00:00");
    }

    void Update()
    {
        timeText.text = "Time : " + hightime;
    }
}
