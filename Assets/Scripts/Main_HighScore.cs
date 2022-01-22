using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_HighScore : MonoBehaviour
{
    private string highscore;
    private Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        highscore = PlayerPrefs.GetString("HighScore", "0");
    }

    void Update()
    {
        scoreText.text = "High Score : " +highscore;
    }
}
