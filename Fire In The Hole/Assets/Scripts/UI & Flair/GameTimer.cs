using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameTimer : MonoBehaviour
{
    [HideInInspector] public float timer;
    public float startingTime;
    public string endScreenScene;

    public TextMeshProUGUI timerText;

    public static GameTimer gameTimer;

    public int[] playerScores;
    public Transform[] playerScoreboards;

    public bool timerStarted = false;
    private void Awake()
    {
        if(gameTimer != null)
        {
            Destroy(gameObject);
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
        timer = startingTime;
        timerStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerStarted) return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            updateTimer(timer);
            if (timer < 10) timerText.color = Color.red;
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
    void EndGame()
    {
        //get each player's final score, then load end screen
        for(int i = 0; i < playerScoreboards.Length; i++)
        {
            playerScores[i] = playerScoreboards[i].gameObject.GetComponentInChildren<ScoreTracker>().score;
        }
        timer = 0;
        timerStarted = false;
        SceneManager.LoadScene(endScreenScene);
    }
}
