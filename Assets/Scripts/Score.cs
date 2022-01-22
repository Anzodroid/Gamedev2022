using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;
    private int scoreInt;
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreInt = GameManagement.Score;
        scoreInt = 0;
    }

    void Update()
    {
        scoreInt = GameManagement.Score;
        scoreText.text = "Score : " + scoreInt;
    }
}
