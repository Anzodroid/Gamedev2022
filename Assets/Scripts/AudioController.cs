using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioClip Intro;
    public AudioClip Normal;
    public AudioClip OrcScared;
    public AudioClip OrcDead;
    public static AudioClip musicClip;
    private static bool music;
    GameManagement gameManagement;

    void Start ()
    {
    gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
    backgroundMusic = GetComponent<AudioSource>();
    music = false;
    }

    public static bool Music
    {
        set { music = value; }
        get { return music; }
    }

    private void Update()
    {
        if (!backgroundMusic.isPlaying && music)
        {
           // Debug.Log("Play Main");
            StartCoroutine(Main());
        }

        if ((!GameManagement.GhostAttack) && backgroundMusic.clip != OrcDead)
            {
                StopCoroutine(Main());

            if (backgroundMusic.clip != OrcScared)
            {
                backgroundMusic.Stop();
            }

            if (!backgroundMusic.isPlaying)
            {
               // Debug.Log("Play Orc Scared");
                backgroundMusic.clip = OrcScared;
                backgroundMusic.volume = 1;
                backgroundMusic.Play();
            }
        }

        if (gameManagement.GhostCount() > 0)
        {
            if (backgroundMusic.clip != OrcDead!)
            {
                backgroundMusic.Stop();
                StopCoroutine(Main());

                if (!backgroundMusic.isPlaying)
                {
                    //Debug.Log("Play OrcDead");
                    backgroundMusic.clip = OrcDead;
                    backgroundMusic.volume = 1;
                    backgroundMusic.Play();
                }
            }
        }

        if (GameManagement.GhostAttack && backgroundMusic.clip !=Normal && gameManagement.GhostCount() == 0)
        {
            backgroundMusic.Stop();
        }
    }

    public IEnumerator Main()
    {
        backgroundMusic.clip = Normal;
        backgroundMusic.loop = true;
        backgroundMusic.volume = 0.5f;
        backgroundMusic.Play();
        yield return new WaitForSeconds(backgroundMusic.clip.length);
    }
}
