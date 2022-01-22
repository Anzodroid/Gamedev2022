using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryStrike : MonoBehaviour
{
    GameManagement gameManagement;
    public GameObject artillery;
    public List<Vector3> Walkable;
    private int walkCount;
    private int strikeChoice;
    private static Vector3 strikeLocation;
    private bool isStrike;
    public static int x;
    public static int y;
    private static int direction;
    private const float centerX = 13.5f;
    private const float centerY = -14.0f;
    public AudioClip hit;
    private AudioSource hitAudio;

    private void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        hitAudio = GetComponent<AudioSource>();
        Walkable = gameManagement.Walkable;
        walkCount = Walkable.Count;
        isStrike = false;
    }

    void Update()
    {
        if (!isStrike && GameManagement.StartMovement == true && AudioController.Music == true)
        {
            isStrike = true;
            StartCoroutine(Incomming());
        }
    }

    private IEnumerator Incomming()
    {
        x = Random.Range(-20, 20);
        y = Random.Range(-20, 20);
        direction = Random.Range(1, 5); // range 1-4

        strikeChoice = UnityEngine.Random.Range(0, walkCount);
        strikeLocation = Walkable[strikeChoice];

        switch (direction)
        {
            case 1:
                {
                    Instantiate(artillery, new Vector3(x + centerX, 5, 0), Quaternion.Euler(0, -120, 0));
                    break;
                }
            case 2:
                {
                    Instantiate(artillery, new Vector3(40, y + centerY, 0), Quaternion.Euler(0, -120, 0));
                    break;
                }
            case 3:
                {
                    Instantiate(artillery, new Vector3(-x + centerX, -33, 0), Quaternion.Euler(0, -120, 0));
                    break;
                }
            case 4:
                {
                    Instantiate(artillery, new Vector3(-15, -y + centerY, 0), Quaternion.Euler(0, -120, 0));
                    break;
                }
        }

        yield return new WaitForSeconds(4);
        isStrike = false;
    }

    public static int Xvalue
    {
        get{return x;}
    }

    public static int Yvalue
    {
        get {return y;}
    }
    public static int Axis
    {
        get {return direction;}
    }

    public static Vector3 StrikeLocation
    {
        set {strikeLocation = value;}
        get {return strikeLocation;}
    }

    public IEnumerator Explosion()
    {
        hitAudio.clip = hit;
        hitAudio.Play();
        yield return new WaitForSeconds(hitAudio.clip.length);  
    }

}
