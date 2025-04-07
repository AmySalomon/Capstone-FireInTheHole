using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
public class GameTimer : MonoBehaviour
{
    [HideInInspector] public float timer;
    private float countdownTimer = 0;
    private float endTimer;
    public float startingTime;
    public float runningOutTime;
    public string endScreenScene;
    public Transform timerUI;
    public TextMeshProUGUI timerText;

    public static GameTimer gameTimer;

    public TextMeshProUGUI countdownText;

    public int[] playerScores;
    public Transform[] playerScoreboards;
    public GameObject[] players;
    public PlayerConfig[] playerStats;

    public bool timerStarted = false;

    private float newVertScale;
    private int count = 10;

    public AudioMixer audioMixer;
    public AudioSource musicPlayer;
    public AudioSource scaryMusicPlayer;
    public AudioClip scaryMusic;
    private float musicLevel;
    private AudioSource myWhistleSFX;
    private bool soundOffWhistle = true;
    private bool playScaryMusic = true;
    private static string currentScene = "Null"; //Detects scene name
    private void Awake()
    {
        myWhistleSFX = GetComponent<AudioSource>();
        if(gameTimer != null)
        {
            Destroy(gameObject);
            Debug.Log("Killed it!");
        }
        else
        {
            gameTimer = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        countdownText.gameObject.transform.localScale = new Vector3(1, 0, 1);
        timer = startingTime;
        timerStarted = true;
        SceneNameUpdate(); //Check scene name
    }

    // Update is called once per frame
    void Update()
    {
        SceneNameUpdate(); //Check scene name
        if (currentScene == "EndingScene")
        {
            Time.timeScale = 1f;
            //Debug.Log(Time.timeScale);
        }
        if (currentScene == "MainMenu")
        {
            Destroy(gameObject);
            Debug.Log("GameTimer: Killed it!");
        }

        if (!timerStarted) return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            updateTimer(timer);
            if (timer < runningOutTime) timerText.color = Color.red;
            if (timer < 11) CountdownTime();


            if (timer < runningOutTime - 3.5f) {/*stops the next line from running after it should, which breaks the countdown*/ }
            else if (timer < runningOutTime - 3.4f)
            {
                countdownText.gameObject.transform.localScale = new Vector3(1, 0, 1);
            }

            else if (timer < runningOutTime - 3)
            {
                //shrink
                newVertScale = Mathf.Lerp(1, 0, (timer - (runningOutTime - 3)) / -0.4f);
                countdownText.gameObject.transform.localScale = new Vector3(1, newVertScale, 1);

            }

            else if (timer < runningOutTime - 0.4f)
            {
                //if the lerp has ended, snap the circle to have the normal scale
                countdownText.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            else if (timer < runningOutTime)
            {
                countdownText.text = "1 MINUTE";
                countdownText.fontSize = 3;
                //grow
                newVertScale = Mathf.Lerp(0, 1, (timer - runningOutTime) / -0.4f);
                countdownText.gameObject.transform.localScale = new Vector3(1, newVertScale, 1);
            }

            //new music management
            audioMixer.GetFloat("MusicParam", out musicLevel);

            if (timer <= runningOutTime)
            {
                if (playScaryMusic == true)
                {
                    //Sub normal music for scary music at 60 seconds left!
                    Destroy(musicPlayer); //Destroy OG music
                    scaryMusicPlayer.PlayOneShot(scaryMusic, 1f); //Play scary music
                    //Debug.Log("WORKS!");
                    playScaryMusic = false;
                }
            }
        }
        else
        {
            EndGame();
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    void CountdownTime()
    {
        countdownTimer += Time.deltaTime;
        countdownText.fontSize = 6;
        countdownText.text = count.ToString();
        if (countdownTimer < 0.1)
        {
            //grow
            newVertScale = Mathf.Lerp(0, 1, countdownTimer / 0.1f);
            countdownText.gameObject.transform.localScale = new Vector3(1, newVertScale, 1);
        }
        
        
        else if (countdownTimer < 0.8)
        {
            //if the lerp has ended, snap the circle to have the normal scale
            countdownText.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        else if (countdownTimer < 1)
        {
            //shrink
            newVertScale = Mathf.Lerp(1, 0, (countdownTimer - 0.8f) / 0.1f);
            countdownText.gameObject.transform.localScale = new Vector3(1, newVertScale, 1);
        }

        else
        {
            //when a second has passed, reset this to the start, and lower the count variable.
            count--;
            countdownTimer = 0;
        }
    }    

    void EndGame()
    {
        PauseMenu.functional = false; //do not let the pause menu work on the end screen <3
        //a small flourish after the game ends before moving to the end screen
        if (currentScene != "EndingScene")
        {
            Time.timeScale = 0;
            //Debug.Log("GameTimer: WE AREN'T IN END");
        }
        //Time.timeScale = 0;
        endTimer += Time.unscaledDeltaTime;
        countdownText.fontSize = 3;
        countdownText.text = "IT'S OVER!";
        //turns off the music, momentarily
        audioMixer.SetFloat("MusicParam", -80);
        if (soundOffWhistle == true)
        {
            myWhistleSFX.Play();
            soundOffWhistle = false;
        }
        if (endTimer < 0.2)
        {
            //shrink
            newVertScale = Mathf.Lerp(0, 1, endTimer / 0.2f);
            countdownText.gameObject.transform.localScale = new Vector3(1, newVertScale, 1);
        }

        if (endTimer > 3)
        {
            //get each player's final score and config info, then load end screen
            for (int i = 0; i < playerScoreboards.Length; i++)
            {
                if (players[i] == null) { break; }
                playerScores[i] = playerScoreboards[i].gameObject.GetComponent<ScoreTracker>().score;
                playerScoreboards[i].gameObject.SetActive(false);
                players[i].SetActive(false);
            }
            timer = 0;
            audioMixer.SetFloat("MusicParam", musicLevel);
            timerStarted = false;
            timerText.gameObject.SetActive(false);
            countdownText.gameObject.SetActive(false);
            timerUI.gameObject.SetActive(false);
            PlayerInputHandler[] playerInputHandler = FindObjectsOfType<PlayerInputHandler>(); //stop all controllers from rumbling!
            foreach (PlayerInputHandler p in playerInputHandler)
            {
                p.StopAllCoroutines();
            }
            SceneManager.LoadScene(endScreenScene);
        }
            
    }

    void SceneNameUpdate() //Update the scene name
    {
        // Get the current scene name
        Scene currentLevel = SceneManager.GetActiveScene();
        currentScene = currentLevel.name;
        //Debug.Log("Loaded Level: " + currentScene);
    }
}
