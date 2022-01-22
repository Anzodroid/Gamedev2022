using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colour : MonoBehaviour
{
    public GameObject MyImage;
    int timeTick;

    private void Start()
    {
        timeTick = 1;
    }

    private void Update()
    {
        if (Time.time >timeTick)
        {
            timeTick += 1;
        }

        NewColour();
    }

    public void NewColour()
    {
        float ticker = timeTick - Time.time; 

        if (ticker< 1f)
        {
            MyImage.GetComponent<Image>().color = new Color(1, ticker, 0, 1 );
        }
        else
        {
            MyImage.GetComponent<Image>().color = new Color(1 ,1, 0, 1);
        }
    }

}
