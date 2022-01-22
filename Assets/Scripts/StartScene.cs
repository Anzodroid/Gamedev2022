using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{

    public void Start()
    {
        Debug.Log("StartScreen");
        Time.timeScale = 1;

    }

    public void loadStartScene()
    {
        if (Time.timeScale == 1)
        {
            SceneManager.LoadScene("StartScene");
            Time.timeScale = 0;
        }
    }


    public void loadLevel(string scene)
    {
        Time.timeScale = 0;
        GameManagement.StartMovement = false;
        SceneManager.LoadScene(scene);
    }
}
