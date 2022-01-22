using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    private Text start;
    private string[] countDown = { "3", "2", "1", "GO!" , ""};

    void Start()
    {
        start = GetComponent<Text>();
        StartCoroutine(Go());
        StartCoroutine(StopTime());
    }

    private IEnumerator Go()
    {
        start.text = (string)countDown.GetValue(0);
        yield return new WaitForSecondsRealtime(1f);
        start.text = (string)countDown.GetValue(1);
        yield return new WaitForSecondsRealtime(1f);
        start.text = (string)countDown.GetValue(2);
        yield return new WaitForSecondsRealtime(1f);
        start.text = (string)countDown.GetValue(3);
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("StartMusic !!!!!!!");
        AudioController.Music = true;
        Debug.Log("StartMovement !!!!!!!");
        GameManagement.StartMovement = true; // the order here could be important 
        Debug.Log("GO!!!!!!!");
        Time.timeScale = 1;
        Debug.Log("timescale 1!!!!!!!");
        gameObject.SetActive(false);    
    }

    private IEnumerator StopTime()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(4f);
    }


}
