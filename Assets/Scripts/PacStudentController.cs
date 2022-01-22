using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private int lastTime;
    private float speed;
    private float movement;
    private float duration;
    private bool teleportL;
    private bool teleportR;
    private bool isMoving;
    private bool powerUp;
    private bool canMove;
    private int hitDirection;

    private KeyCode currentInput;
    private KeyCode lastInput;
    private Vector3 currentPos;
    private Vector3 NormPos;
    private Vector3 localPos;
    private Vector3 lerpPos;
    private static Vector3 pacPosition;
    public List<Vector3> Walkable;
    public List<Vector3> GhostArea;

    private Tween tween;
    private Animator anim;
    public AudioClip movement_FX;
    public AudioClip pellet_FX;
    public AudioClip wall_FX;
    public AudioClip die_FX;
    public AudioClip bonus_FX;
    public AudioClip hit_FX;
    public AudioClip frozen;
    private AudioSource pacaudio;
    private GameObject[] gameObjects;
    public ParticleSystem dust;
    public ParticleSystem wallHit;
    public ParticleSystem die;
    private BoxCollider boxCollider;
    GameManagement gameManagement;
    private bool isAlive;
    SpriteRenderer SpriteRenderer;
    private  Rigidbody rBody;


    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.color = Color.white;
        isAlive = true;
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        powerUp = false;
        canMove = true;
        teleportL = false;
        teleportR = false;
        hitDirection = 5;
        speed = 2.5f;
        lastInput = KeyCode.None;
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        pacaudio = GetComponent<AudioSource>();
        currentPos = new Vector3(1f, -1f, 0f);
        gameObjects = GameObject.FindGameObjectsWithTag("Walkable");

        foreach (GameObject item in gameObjects)
        {
            Walkable.Add(item.transform.position);
            if (item.name.Contains("GhostArea"))
                GhostArea.Add(item.transform.position);
        }
    }

    public static Vector3 PacPosition
    {
    set { pacPosition = value;  }
    get { return pacPosition; }
    }


    private void FixedUpdate()
    {
        duration = 1 / speed;
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
        pacPosition = currentPos;
        movement = speed * Time.deltaTime;
        
        StartCoroutine(IsMoving());

        if (isMoving)
        {
            if (!pacaudio.isPlaying)
            {
                StartCoroutine(MoveSound());
            }
        }

        if (tween != null)
        {
            float distance = Vector3.Distance(tween.Target.position, tween.EndPos);
            NormPos = (tween.EndPos - tween.StartPos).normalized;

            if (distance > 0)
            {
                tween.Target.position = currentPos;
            }
            else
            {
                tween = null;
            }
        }

        if (currentInput == KeyCode.None || tween == null)
        {
                if (Direction(lastInput))
                {
                    currentInput = lastInput;
                }

                else
                {
                   Direction(currentInput);
                }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
           lastInput = KeyCode.A;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.A;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = KeyCode.D;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.D;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = KeyCode.W;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.W;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = KeyCode.S;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.S;
            }
        }
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        if (tween == null && GameManagement.StartMovement && canMove)
        {
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }

    public bool Direction(KeyCode key)
    {
            if (teleportL == true && NormPos.x == -1)
            {
                teleportL = false;
                AddTween(transform, new Vector3(27f, -14, 0), new Vector3(26f, -14, 0), duration);
                return true;
            }

            else if (teleportR == true && NormPos.x == +1)
            {
                teleportR = false;
                AddTween(transform, new Vector3(0, -14, 0), new Vector3(1f, -14, 0), duration);
                return true;
            }
            else
            {
                teleportL = false;
                teleportR = false;

                int x = (int)Math.Round(localPos.x);
                int y = (int)Math.Round(localPos.y);
                float z = localPos.z;

                if (key == KeyCode.A)
                {
                    lerpPos = new Vector3(x - 1, y, z);
                    if (MoveCheck(lerpPos))
                    {
                        anim.SetBool("left", true);
                        anim.SetBool("down", false);
                        anim.SetBool("up", false);
                        anim.SetBool("right", false);
                        hitDirection = 1;
                        boxCollider.center = new Vector3(-0.1f, 0, 0); // adjust box
                        AddTween(transform, localPos, new Vector3(x - 1, y, z), duration);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (key == KeyCode.D)
                {
                    lerpPos = new Vector3(x + 1, y, z);
                    if (MoveCheck(lerpPos))
                    {
                        anim.SetBool("right", true);
                        anim.SetBool("down", false);
                        anim.SetBool("left", false);
                        anim.SetBool("up", false);
                        hitDirection = 2;
                        boxCollider.center = new Vector3(0.1f, 0, 0); // adjust box
                        AddTween(transform, localPos, new Vector3(x + 1, y, z), duration);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (key == KeyCode.W)
                {
                    lerpPos = new Vector3(x, y + 1, z);
                    if (MoveCheck(lerpPos))
                    {
                        anim.SetBool("up", true);
                        anim.SetBool("down", false);
                        anim.SetBool("left", false);
                        anim.SetBool("right", false);
                        hitDirection = 3;
                        boxCollider.center = new Vector3(0, 0.1f, 0); // adjust box
                        AddTween(transform, localPos, new Vector3(x, y + 1, z), duration);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (key == KeyCode.S)
                {
                    lerpPos = new Vector3(x, y - 1, z);
                    if (MoveCheck(lerpPos))
                    {
                        anim.SetBool("down", true);
                        anim.SetBool("up", false);
                        anim.SetBool("left", false);
                        anim.SetBool("right", false);

                        hitDirection = 4;
                        boxCollider.center = new Vector3(0, -0.1f, 0); // adjust box
                        AddTween(transform, localPos, new Vector3(x, y - 1, z), duration);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (key == KeyCode.None)
                {
                    return false;
                }
            }

        return false;
    }


    public bool MoveCheck(Vector3 lerp)
    {
        if (Walkable.Contains(lerp) && !GhostArea.Contains(lerp))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator IsMoving()
    {
        Vector3 startPos = gameObject.transform.position;
        yield return new WaitForSeconds(0.2f);
        Vector3 lastPos = transform.position;
        if (startPos.x != lastPos.x || startPos.y != lastPos.y)
        {
            isMoving = true;
            dust.Play();
        }
        else
        {
            isMoving = false;
            dust.Stop();
        }
    }

    private IEnumerator MoveSound()
    {
        yield return new WaitForSeconds(0.000001f);
     
        if (!pacaudio.isPlaying)
        {
            pacaudio.volume = 0.5f;
            pacaudio.clip = movement_FX;
            pacaudio.Play();
        }
    }


    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.name == "TeleportL")
        {
            teleportL = true;
        }

        if (trigger.name == "TeleportR")
        {
            teleportR = true;
        } 

        if (trigger.gameObject.name.Contains("Pellet"))
        {
          //  Destroy(trigger.gameObject);
         //   GameManagement.Score += 10;
         //   GameManagement.Pellets -= 1;
         //   pacaudio.clip = pellet_FX;
          //  pacaudio.volume = 0.3f;
           // pacaudio.Play();
        }

        if (trigger.gameObject.name.Contains("CherryBurger"))
        {
           // Destroy(trigger.gameObject);
           // GameManagement.Score += 100;
        }

        if (trigger.gameObject.name.Contains("Burger"))
        {
           // pacaudio.volume = 0.5f;
          //  GameManagement.Score += 200;
           // trigger.enabled = false;
           // pacaudio.clip = bonus_FX;
           // pacaudio.Play();
            //trigger.gameObject.SetActive(false);
        }

        if (trigger.gameObject.CompareTag("Walls"))
        {
            pacaudio.volume = 0.5f;
            pacaudio.clip = wall_FX;
            pacaudio.Play();
            WallHit();
        }

        if (trigger.gameObject.name.Contains("PowerFlash"))
        {
            powerUp = true;
            Invoke("PowerUp", 10.0f);
            Destroy(trigger.gameObject);
            GameManagement.ScaredTime = 10f;
            GameManagement.Scared = true;
            GameManagement.GhostAttack = false;
        }

        if (trigger.gameObject.CompareTag("Enemy") && !powerUp && isAlive && !gameManagement.deadGhosts.Contains(trigger.gameObject.name))
        {
            rBody.detectCollisions = false;
            isAlive = false;
            pacaudio.clip = die_FX;
            pacaudio.Play();
            anim.SetTrigger("isDead");
            anim.SetBool("down", false);
            anim.SetBool("up", false);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            GameManagement.Life -= 1;
            Debug.Log("GameLives :" + GameManagement.Life);
            tween = null;
            canMove = false;
            Invoke("startMove", 4f);
            StartCoroutine(PacDie());
            currentInput = KeyCode.None;
            lastInput = KeyCode.None;
        }

        if (trigger.gameObject.CompareTag("Enemy") && powerUp && !gameManagement.deadGhosts.Contains(trigger.gameObject.name))
        {
            string dead = trigger.gameObject.name;
            gameManagement.DeadGhost(dead);
        }

        if (trigger.gameObject.name.Contains("ArtilleryBall"))
        {
            
            GhostInnovation.lockOn = false;  // check this doenst break level 1
            rBody.detectCollisions = false;
            isAlive = false;
            pacaudio.volume = 1f;
            pacaudio.clip = die_FX;
            pacaudio.Play();
            anim.SetTrigger("isDead");
            anim.SetBool("down", false);
            anim.SetBool("up", false);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            SpriteRenderer.color = Color.red;
            GameManagement.Life -= 1;
            Debug.Log("GameLives :" + GameManagement.Life);
            tween = null;
            canMove = false;
            Invoke("startMove", 4f);
            StartCoroutine(PacDie());
            currentInput = KeyCode.None;
            lastInput = KeyCode.None;
        }


        if (trigger.gameObject.name.Contains("MagicBall"))
        {
            pacaudio.clip = frozen;
            pacaudio.volume = 1;
            pacaudio.Play();
            speed = 1.3f;
            SpriteRenderer.color = Color.cyan;
            Invoke("Unfreeze", 3.0f);
        }
    }

    void Unfreeze()
    {
        speed = 2.5f;
        SpriteRenderer.color = Color.white;
    }
    void PowerUp()
    {
        powerUp = false;
        Debug.Log("powerUp - False");
    }

    IEnumerator PacDie()
    {
        GhostInnovation.lockOn = false;
        die.Play();
        yield return new WaitForSeconds(3f);
        SpriteRenderer.color = Color.white;
        anim.SetBool("up", true);
        gameObject.transform.position = new Vector3(1, -1, 0);
        isAlive = true;

    }

    void startMove()
    {
        rBody.detectCollisions = true;
        canMove = true;  
    }

    private void WallHit()
    {
        switch (hitDirection)
        {
            case 1:
                {
                    wallHit.transform.position = new Vector3(localPos.x - 0.7f, localPos.y, 0f);
                    wallHit.Play();
                    break;
                }
            case 2:
                {
                    wallHit.transform.position = new Vector3(localPos.x + 0.7f, localPos.y, 0f);
                    wallHit.Play();
                    break;
                }
            case 3:
                {
                    wallHit.transform.position = new Vector3(localPos.x, localPos.y + 0.7f, 0f);
                    wallHit.Play();
                    break;
                }
            case 4:
                {
                    wallHit.transform.position = new Vector3(localPos.x, localPos.y - 0.7f, 0f);
                    wallHit.Play();
                    break;
                }
            case 5:
                {
                    wallHit.Stop();
                    break;
                }
        }
    }
}