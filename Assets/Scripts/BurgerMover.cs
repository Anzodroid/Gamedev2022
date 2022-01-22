using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerMover : MonoBehaviour
{
    private Tween tween;
    private Vector3 localPos;
    private Vector3 currentPos;
    private const float centerX = 13.5f;
    private const float centerY = -14.0f;
    private int x;
    private int y;
    private int direction;
    private float destroyX1;
    private float destroyY1;
    private float destroyX2;
    private float destroyY2;
    private int duration;

    void Start()
    {
        duration = Random.Range(8, 11); // max Exclusive for int
        destroyX1 = -99;
        destroyX2 = 99;
        destroyY1 = -99;
        destroyY2 = 99;

        x = CherryController.Xvalue;
        y = CherryController.Yvalue;
        direction = CherryController.Axis;
    }

    private void FixedUpdate()
    {
        localPos = transform.localPosition;

        if (tween != null)
        {
            float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
            transform.position = currentPos;
        }
    }

    void Update()
    {
        x = CherryController.Xvalue;
        y = CherryController.Yvalue;
        direction = CherryController.Axis;

        if (localPos.x <= destroyX1 || localPos.x >= destroyX2 || localPos.y <= destroyY1 || localPos.y >= destroyY2)
        {
            Destroy(transform.gameObject);
        }

        switch (direction)
        {
            case 1:
                {
                    // --> down
                    destroyX1 = -34;
                    destroyX2 = 40;
                    destroyY1 = -30; // main target
                    destroyY2 = 99;
                    AddTween(transform, transform.localPosition, new Vector3((-x) + centerX, -33, 0), duration);
                    break;
                }

            case 2:
                {
                    // --> left
                    destroyX1 = -14; // main target
                    destroyX2 = 99;   
                    destroyY1 = -35;
                    destroyY2 = 7;
                    AddTween(transform, transform.localPosition, new Vector3(-15, (-y) + centerY, 0), duration);
                    break;
                }
            case 3:
                {
                    // --> up
                    destroyX1 = -34;
                    destroyX2 = 40;
                    destroyY1 = -99; 
                    destroyY2 = 2;  // main target
                    AddTween(transform, transform.localPosition, new Vector3((x) + centerX, 5, 0), 10);
                    break;
                }

            case 4:
                {
                    // --> right
                    destroyX1 = -99;
                    destroyX2 = 40;  // main target
                    destroyY1 = -35; 
                    destroyY2 = 7;
                    AddTween(transform, transform.localPosition, new Vector3(45, (y) + centerY, 0), 8);
                    break;
                }
        }
  
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        
        if (tween == null)
        {
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }


}


