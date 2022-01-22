using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    private Image life;
    private int lifeCount;

    void Start()
    {
        life = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        lifeCount = GameManagement.Life;

        if (lifeCount< 3)
        {
            if (lifeCount >= 0)
            {
            life = gameObject.transform.GetChild(lifeCount).GetComponent<Image>();
            Destroy(life);
            }
        }
    }
}
