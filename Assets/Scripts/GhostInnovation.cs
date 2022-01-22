using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInnovation : MonoBehaviour
{
    public GameObject fire;
    private int lastTime;
    //private float timer = -0.0f;
    private float speed;
    private float movement;
    private float duration;
    private bool isMoving;
    private bool ghostArea;
    private Vector3 currentPos;
    private Vector3 NormPos;
    private Vector3 localPos;
    public List<Vector3> Walkable;
    public List<Vector3> GhostArea;
    public List<Vector3> StartGhostArea;
    public List<Vector3> GhostAreaExitA;
    public List<Vector3> spawnExitTarget;
    private float exitDistance;
    private Tween tween;
    private Animator anim;
    private GameObject[] gameObjects;
    private BoxCollider boxCollider;
    private int currentInput;
    private Vector3 lastPosition;
    private string enemyName;
    private Vector3 ghostPos;
    private Vector3 up;
    private Vector3 down;
    private Vector3 left;
    private Vector3 right;
    public List<Vector3> ghostOptions;
    private int ghostThought;
    private Vector3 ghostLocation;
    private Vector3 direction;
    private Vector3 previousDirection;
    private Vector3 lastDirection;
    private Vector3 startingPos;
    private Vector3 pacPosition;
    private Vector3 spawnExit;
    public bool isAlive;
    private bool isScared;
    private bool isRecovery;
    private float targetDistance;
    PacStudentController pacman;
    GameManagement gameManagement;
    private bool attack;
    private int hitDirection;
    private Vector3 fireballStart;
    public static bool lockOn;
    SpriteRenderer SpriteRenderer;

    void Start()
    {
        lockOn = false;
        localPos = transform.localPosition;
        isAlive = true;
        ghostArea = true;
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        pacman = gameObject.GetComponent<PacStudentController>(); // access pacman script
        speed = 1.7f;
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        ghostLocation = transform.position;
        gameObjects = GameObject.FindGameObjectsWithTag("Walkable");
        enemyName = transform.name;
        Walkable = gameManagement.Walkable;
        GhostArea = gameManagement.GhostArea;
       // GhostAreaExitA = gameManagement.GhostAreaExitA;
        attack = true;
        SpriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyName == "GhostA")
        {
            startingPos = new Vector3(12f, -14f, 0f);
            spawnExit = new Vector3(13f, -17f, 0f); //not used
        }

        if (enemyName == "GhostB")
        {
            startingPos = new Vector3(13f, -14f, 0f);
            spawnExit = new Vector3(13f, -11f, 0f);  //not used
        }

        if (enemyName == "GhostC")
        {
            startingPos = new Vector3(14f, -14f, 0f);
            spawnExit = new Vector3(14f, -11f, 0f);  //not used
        }

        if (enemyName == "GhostD")
        {
            startingPos = new Vector3(15f, -14f, 0f);
            spawnExit = new Vector3(14f, -17f, 0f);  //not used
        }
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
        RaycastHit();

        movement = speed * Time.deltaTime;

        if (tween != null && GameManagement.StartMovement == true)
        {
            float distance = Vector3.Distance(tween.Target.position, tween.EndPos);
            NormPos = (tween.EndPos - tween.StartPos).normalized;
            previousDirection = NormPos;

            if (distance > 0)
            {
                tween.Target.position = currentPos;
            }
            else
            {
                tween = null;
            }
        }

        if (tween == null)
        {
            if (GameManagement.StartMovement == true && isAlive)
            {
                GhostBrain();
            }

            else if (!isAlive)
            {
                lockOn = false;
                ghostArea = true;
                AddTween(transform, localPos, startingPos, 10); // send to ghost area
            }
        }

        if (GameManagement.Scared)
        {
            isScared = true;
        }

        if (GameManagement.Recovery)
        {
            isScared = false;
            isRecovery = true;
        }
        if (GameManagement.GhostAttack)
        {
            isScared = false;
            isRecovery = false;
        }

        if (isAlive)
        {
            if (NormPos == new Vector3(-1, 0, 0))
            {
                anim.SetBool("left", true);
                anim.SetBool("down", false);
                anim.SetBool("up", false);
                anim.SetBool("right", false);
                anim.SetBool("isDead", false);
            }
            if (NormPos == new Vector3(1, 0, 0))
            {
                anim.SetBool("right", true);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("up", false);
                anim.SetBool("isDead", false);
            }
            if (NormPos == new Vector3(0, 1, 0))
            {
                anim.SetBool("up", true);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
                anim.SetBool("isDead", false);
            }
            if (NormPos == new Vector3(0, -1, 0))
            {
                anim.SetBool("down", true);
                anim.SetBool("up", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
                anim.SetBool("isDead", false);
            }

            if (isScared)
            {
                anim.SetBool("up", false);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
                anim.SetBool("isScared", true);
                anim.SetBool("isDead", false);
            }
            if (isRecovery)
            {
                anim.SetBool("up", false);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
                anim.SetBool("isScared", false);
                anim.SetBool("isRecovery", true);
                anim.SetBool("isDead", false);
            }
        }
        else if (!isAlive)
        {
            anim.SetBool("down", false);
            anim.SetBool("up", false);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("isScared", true);
            anim.SetBool("isRecovery", true);
            anim.SetBool("isScared", false);
            anim.SetBool("isRecovery", false);
            anim.SetBool("isDead", true);
        }
        else
        {
            Debug.Log("unkown State!!" + GameManagement.Recovery + " " + GameManagement.Scared + " " + isAlive);
        }
    }

    private void GhostBrain()
    {
        // avoid rounding point errors
        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;
        up = new Vector3(x, y + 1, z);
        down = new Vector3(x, y - 1, z);
        left = new Vector3(x - 1, y, z);
        right = new Vector3(x + 1, y, z);

        if (ghostArea == false)
        {
            if (lastDirection.y == 1 && lastDirection.x == 0) // up
            {

                hitDirection = 3;

                if (Walkable.Contains(up) && !GhostArea.Contains(up)) // up
                    ghostOptions.Add(up);

                if (Walkable.Contains(left) && !GhostArea.Contains(left)) // left
                    ghostOptions.Add(left);

                if (Walkable.Contains(right) && !GhostArea.Contains(right)) // right
                    ghostOptions.Add(right);

                moveGhost();
            }

            else if (lastDirection.y == -1 && lastDirection.x == 0) // down
            {
                hitDirection = 4;

                if (Walkable.Contains(down) && !GhostArea.Contains(down)) // down
                    ghostOptions.Add(down);

                if (Walkable.Contains(left) && !GhostArea.Contains(left)) // left
                    ghostOptions.Add(left);

                if (Walkable.Contains(right) && !GhostArea.Contains(right)) // right
                    ghostOptions.Add(right);

                moveGhost();
            }

            else if (lastDirection.x == -1 && lastDirection.y == 0) // left
            {

                hitDirection = 1;

                if (Walkable.Contains(up) && !GhostArea.Contains(up)) // up
                    ghostOptions.Add(up);

                if (Walkable.Contains(down) && !GhostArea.Contains(down)) // down
                    ghostOptions.Add(down);

                if (Walkable.Contains(left) && !GhostArea.Contains(left)) // left
                    ghostOptions.Add(left);

                moveGhost();
            }

            else if (lastDirection.x == 1 && lastDirection.y == 0) // right
            {

                hitDirection = 2;

                if (Walkable.Contains(down) && !GhostArea.Contains(down)) // down
                    ghostOptions.Add(down);

                if (Walkable.Contains(right) && !GhostArea.Contains(right)) // right
                    ghostOptions.Add(right);

                if (Walkable.Contains(up) && !GhostArea.Contains(up)) // up
                    ghostOptions.Add(up);

                moveGhost();
            }

        }
        if (ghostArea == true)
        {
            if (Walkable.Contains(up)) // up
                ghostOptions.Add(up);

            if (Walkable.Contains(down)) // down
                ghostOptions.Add(down);

            if (Walkable.Contains(right)) // right
                ghostOptions.Add(right);

            if (Walkable.Contains(left)) // left
                ghostOptions.Add(left);


            moveGhost();
        }
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        if (tween == null && GameManagement.StartMovement == true)
        {
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }

    private void ConfusedGhost()
    {
        Debug.Log("Ghost has no other option - May need to turn around");

        if (Walkable.Contains(up)) // up
            ghostOptions.Add(up);

        if (Walkable.Contains(down)) // down
            ghostOptions.Add(down);

        if (Walkable.Contains(right)) // right
            ghostOptions.Add(right);

        if (Walkable.Contains(left)) // left
            ghostOptions.Add(left);

        if (ghostOptions.Count == 0)
        {
            Debug.Log("Very confused ghost ");
        }
    }

    private void moveGhost()
    {
        // rounding implemented to avoid rounding point errors
        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;

        up = new Vector3(x, y + 1, z);
        down = new Vector3(x, y - 1, z);
        left = new Vector3(x - 1, y, z);
        right = new Vector3(x + 1, y, z);

        if (enemyName == "GhostA" && !lockOn ||  enemyName == "GhostB" && !lockOn || enemyName == "GhostC" && !lockOn)
        {

            if (ghostOptions.Count == 0)
            {
                ConfusedGhost();
            }
            ghostThought = UnityEngine.Random.Range(0, ghostOptions.Count);

            direction = ghostOptions[ghostThought];
        }
        if (enemyName == "GhostD"  || enemyName == "GhostB" && ghostArea ||  enemyName == "GhostB" && lockOn || enemyName == "GhostA" && lockOn && !ghostArea || enemyName == "GhostC" && lockOn && !ghostArea)
        {
            if (ghostOptions.Count == 0)
            {
                ConfusedGhost();
            }

            pacPosition = PacStudentController.PacPosition;
            float pacDistance = Vector3.Distance(ghostOptions[0], pacPosition); //benchmark distance
            targetDistance = pacDistance;
            direction = ghostOptions[0];

            foreach (Vector3 option in ghostOptions)
            {
                pacDistance = Vector3.Distance(option, pacPosition);

                if (targetDistance > pacDistance)
                {
                    targetDistance = pacDistance;
                    direction = option;
                }
            }
        }

        if ((!GameManagement.GhostAttack) ||( enemyName == "GhostA" && ghostArea) || (enemyName == "GhostC" && ghostArea))
        {
            if (ghostOptions.Count == 0)
            {
                ConfusedGhost();
            }
                pacPosition = PacStudentController.PacPosition;
                float pacDistance = Vector3.Distance(ghostOptions[0], pacPosition); //benchmark distance
                targetDistance = pacDistance;
                direction = ghostOptions[0];

                foreach (Vector3 option in ghostOptions)
                {
                    pacDistance = Vector3.Distance(option, pacPosition);

                    if (targetDistance < pacDistance)
                    {
                        targetDistance = pacDistance;
                        direction = option;
                    }
                }          
        }

        lastDirection = direction - new Vector3(x, y, z);

        AddTween(transform, new Vector3(x, y, z), direction, duration);
        ghostOptions.Clear();
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.name.Contains("PacStudent"))
        {
            if (!GameManagement.GhostAttack)
            {
                Debug.Log(enemyName + "is Dead");
                isAlive = false;
                lockOn = false;
                anim.SetBool("isDead", true); // duplicate
                                              //AudioController.GhostDead++;
                GameManagement.Score += 500;
            }
        }

            if (trigger.name.Contains("GhostArea") && !isAlive && GameManagement.StartMovement == true)
            {
                isAlive = true;

                if (AudioController.Music && isAlive) // music used to trigger start and prevent null error.
                {
                    try
                    {
                        gameManagement.AliveGhost(enemyName); // investigate rare error
                    }
                    catch
                    {
                        Debug.Log(enemyName + "is causing some drama");
                        gameManagement.AliveGhost(enemyName);
                    }
                }
            }

            if (trigger.name.Contains("SpawnExit"))
            {
                ghostArea = false;
            }

            if (trigger.gameObject.name.Contains("ArtilleryBall"))
            {
                Debug.Log(enemyName + " is hit from Artillery");
                speed = 1f;
                SpriteRenderer.color = Color.red;
                Invoke("Unfreeze", 5.0f);
            }    
    }

        public void RaycastHit() // an invisible ray (line) that is cast (drawn) from one point to another in 3D space
        {
            RaycastHit hitInfo;

            bool raycastHits = Physics.Raycast(localPos, NormPos, out hitInfo, 20f);
        
            if (raycastHits == true && isAlive && !GameManagement.Scared)
            {
                if (hitInfo.transform.gameObject.name.Contains("PacStudent"))
                {
                lockOn = true;
                    if (attack)
                    {
                    attack = false;
                    StartCoroutine(PacAttack());
                    }
                }
            }
        }

    public static bool LockOn
    {
        set { lockOn = value; }
        get { return lockOn; }
    }

    IEnumerator PacAttack()
    {
            switch (hitDirection)
            {
                case 1:
                    {
                        fireballStart = transform.position = new Vector3(transform.localPosition.x - 0.25f, transform.localPosition.y, 0f);
                        break;
                    }
                case 2:
                    {
                        fireballStart = transform.position = new Vector3(transform.localPosition.x + 0.25f, transform.localPosition.y, 0f);
                        break;
                    }
                case 3:
                    {
                        fireballStart = transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.25f, 0f);;
                        break;
                    }
                case 4:
                    {
                        fireballStart = transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.25f, 0f);
                        break;
                    }
                case 5:
                    {
                        break;
                    }
            }
        
        Instantiate(fire, fireballStart, Quaternion.Euler(0, -120, 0));
        yield return new WaitForSecondsRealtime(2);
        attack = true;
    }

    void Unfreeze()
    {
        speed = 1.7f;
        SpriteRenderer.color = Color.white;
    }
}
