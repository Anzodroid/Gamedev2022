using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery : MonoBehaviour
{
    private Tween tween;
    private Vector3 currentPos;
    private int x;
    private int y;
    private int direction;
    private Vector3 targetLocation;
    private float targetDistance;
    private int duration;
    public ParticleSystem fire;
    public ParticleSystem fireTail;
    private bool fireTailPlay;
    private bool firePlay;
    public AudioClip hit;
    private AudioSource hitAudio;
    private bool hitTarget;

    void Start()
    {
        duration = Random.Range(5, 7); // max Exclusive for int
        fireTailPlay =false;
        firePlay = false;
        hitTarget = false;
        hitAudio = GetComponent<AudioSource>();
        targetLocation = ArtilleryStrike.StrikeLocation;
    }

    private void FixedUpdate()
    {
        targetDistance = Vector3.Distance(transform.localPosition, targetLocation);

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
        if (!fireTailPlay)
        {
            //fireTail.Play();
        }

        if (!firePlay)
        {
            fire.Play();
        }

        if (targetDistance <1f && targetDistance !=0)
        {
            hitTarget = true;
        }

        if (hitTarget)
        {
            if (!hitAudio.isPlaying)
            {
                hitAudio.clip = hit;
                hitAudio.Play();   
            }
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
