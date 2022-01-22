using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class QuitGame : MonoBehaviour
{
    public Button quitButton;

    public void Start()
    {
        quitButton = GameObject.Find("QuitGame").GetComponent<Button>();
        quitButton.onClick.AddListener(Quit);
    }

    public void Quit()
    {
        Application.Quit(); // stops game
        //UnityEditor.EditorApplication.isPlaying = false; // causes built to crash
    }

}
