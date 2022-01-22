using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    private static int score;
    private static float scaredTime;
    private static bool scared;
    private static bool recovery;
    public static bool ghostAttack;
    public static int lives;
    private string finalTime;
    private string highScore;
    private string bestTime;
    private DateTime previousTime;
    private DateTime currentTime;
    private GameObject[] walkableGameObjects;
    private GameObject gameOver;
    private GameObject ghostTime;
    private int startingPellets; 
    private int previousBest;
    private static int pellets;
    private static bool startMovement;
    public AudioClip gameOverClip;
    AudioSource gameOverMusic;
    public float timer;
    public float lastTime;
    public List<string> deadGhosts;
    public static int deadGhostCount;
    private GameObject[] gameObjects;
    public List<Vector3> Walkable;
    public List<Vector3> GhostArea;
   // public List<Vector3> GhostAreaExitA;


    private void Awake()
    {
        Time.timeScale = 0;
        startMovement = false;

        gameObjects = GameObject.FindGameObjectsWithTag("Walkable");
        foreach (GameObject item in gameObjects)
        {
            Walkable.Add(item.transform.position);
            if (item.name.Contains("GhostArea"))
                GhostArea.Add(item.transform.position);
/*            if (item.name.Contains("ExitA"))
                GhostAreaExitA.Add(item.transform.position);  */
        }
    }

    private void Start()
    {
        lives = 3;
        if (PlayerPrefs.GetString("HighScore") == null)
        {
            PlayerPrefs.SetString("HighScore", "0");
        }

        highScore = PlayerPrefs.GetString("HighScore");
        Debug.Log("high score" + highScore);

        try
        {
            previousBest = int.Parse(highScore); // does not seem to load on the first build.
        }
        catch
        {
            previousBest = 0;
        }
        
        bestTime = PlayerPrefs.GetString("FastestTime");

        ghostTime = GameObject.Find("GhostTime");
        walkableGameObjects = GameObject.FindGameObjectsWithTag("Walkable");


        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);
        ghostTime.SetActive(false);
        score = 0;
        scaredTime = 0;
    
        gameOverMusic = GetComponent<AudioSource>();
        gameOverMusic.clip = gameOverClip;
        ghostAttack = true;

        gameObjects = GameObject.FindGameObjectsWithTag("Walkable");
   
        foreach (GameObject obj in walkableGameObjects)
        {
            if (obj.name.Contains("Pellet"))
            {
                startingPellets++;
            }
        }
        pellets = startingPellets-1;
        Debug.Log("Starting Pellets :" + pellets);
    }

    private void Update()
    {
        
       // timer += Time.deltaTime;

/*        if ((int)timer > lastTime)
        {
            if (lastTime >= 0)
            {
            }
            lastTime = (int)timer;
        }
*/

        if (Scared == true)
        {
            recovery = false;
            ghostTime.SetActive(true);
            ghostAttack = false;
            ScaredTime -= Time.deltaTime;

            if (ScaredTime <= 3)
            {
                scared = false;
                recovery = true;
            }
        }

        if (Recovery == true)
        {
            ScaredTime -= Time.deltaTime;

            if (ScaredTime <= 0)
            {
                ghostAttack = true;
                Recovery = false;
                ghostTime.SetActive(false);
            }
        }

        if (lives == 0 || pellets ==0 )
        {
            
            GameResults();
        }
    }

    public static bool StartMovement
    {
        set { startMovement = value; }
        get { return startMovement; }
    }

    public static int Pellets
    {
        set { pellets = value; }
        get { return pellets; }
    }

    public static int Life
    {
        set { lives = value; }
        get { return lives; }
    }

    public static int Score
    {
        set { score = value; }
        get { return score; }
    }

    public static float ScaredTime
    {
        set { scaredTime = value; }
        get { return scaredTime; }
    }

    public static bool Scared
    {
        set { scared = value; }
        get { return scared; }
    }

    public static bool Recovery
    {
        set { recovery = value; }
        get { return recovery; }
    }


    public static bool GhostAttack
    {
        set { ghostAttack = value; }
        get { return ghostAttack; }
    }


    public void DeadGhost(string ghostName)
    {
        deadGhosts.Add(ghostName);
        Debug.Log(" Ghost added :" + ghostName + " Total ghosts :" + deadGhosts.Count);
    }

    public void AliveGhost(string ghostName)
    {
        deadGhosts.Remove(ghostName);
      Debug.Log(" Ghost :" + ghostName + " Removed Total ghosts :" + deadGhosts.Count);
    }

    public int GhostCount()
    {
        return deadGhosts.Count;
    }

    public void GameResults()
    {
        finalTime = GameTime.finalTime;
        startMovement = false;
        gameOver.SetActive(true);
        StartCoroutine(Results());
        Invoke("StartScreen", 3f);
    }

    private void StartScreen()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();  // stops game
     // UnityEditor.EditorApplication.isPlaying = false; // this causes build to crash
    }

    IEnumerator Results()
    {
        yield return new WaitForSecondsRealtime(1);

/*        Debug.Log("PREF bestscore " + previousBest + " PREF best time " + bestTime);
        Debug.Log("gamescore " + score + " gametime " + finalTime);*/

        if (score > previousBest)
        {
            PlayerPrefs.SetString("FastestTime", finalTime);
            PlayerPrefs.SetString("HighScore", score.ToString());
           // Debug.Log("New High Score : " + score +" Time :" + finalTime);
        }

        if (score == previousBest)
        {
            var cultureInfo = new CultureInfo("en-AU");           
            previousTime = DateTime.ParseExact(bestTime, "hh:mm:ss", cultureInfo);
            currentTime = DateTime.ParseExact(finalTime, "hh:mm:ss", cultureInfo);

         //  Debug.Log("oldtime" + previousTime + " newtime " + currentTime);

            if (currentTime < previousTime)
            {
                PlayerPrefs.SetString("FastestTime", finalTime);
                //Debug.Log("New Fastest Time " + finalTime);
            }
        }

    }










}
