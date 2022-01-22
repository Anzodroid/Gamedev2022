using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    private Tween tween;
    private Vector3 currentPos;
    private int x;
    private int y;
    private int direction;
    private Vector3 targetLocation;
    private float targetDistance;
    private float duration;
    private bool fireTailPlay;
    private bool firePlay;
    public AudioClip hit;
    private AudioSource hitAudio;
    private bool hitTarget;
    private Vector3 pacPosition;

    void Start()
    {
        hitTarget = false;
        hitAudio = GetComponent<AudioSource>();
        targetLocation = pacPosition = PacStudentController.PacPosition;
        targetDistance = Vector3.Distance(transform.localPosition, targetLocation);
        duration = (targetDistance / targetDistance) * 1.5f;
        hitAudio.clip = hit;
        hitAudio.Play();
    }

    private void FixedUpdate()
    {
        if (tween != null)
        {
            if (targetDistance > 0.4f)
            {
                float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
                currentPos = (timeFraction * timeFraction * timeFraction) * (targetLocation - tween.StartPos) + tween.StartPos;
                transform.position = currentPos;
            }
            else
            {
                tween = null;
                transform.position = targetLocation;
            }
        }
    }

    void Update()
    {
        if (targetDistance < 5f && targetDistance != 0)
        {
            hitTarget = true;
        }

        if (hitTarget)
        {
            hitTarget = false;
            StartCoroutine(Explode());
        }

        AddTween(this.transform, this.transform.localPosition, targetLocation, duration);
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        if (tween == null)
        {
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSecondsRealtime(8);
        Destroy(this.transform.gameObject);
    }
}
