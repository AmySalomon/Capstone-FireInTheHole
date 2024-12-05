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
    public float runningOutTime;
    public string endScreenScene;
    public Transform timerUI;
    public TextMeshProUGUI timerText;

    public static GameTimer gameTimer;

    public int[] playerScores;
    public Transform[] playerScoreboards;
    public GameObject[] players;

    public bool timerStarted = false;
    private void Awake()
    {
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
            if (timer < runningOutTime) timerText.color = Color.red;
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
            if(players[i] == null) { break; }
            playerScores[i] = playerScoreboards[i].gameObject.GetComponent<ScoreTracker>().score;
            playerScoreboards[i].gameObject.SetActive(false);
            players[i].SetActive(false);
        }
        timer = 0;
        timerStarted = false;
        timerText.gameObject.SetActive(false);
        timerUI.gameObject.SetActive(false);
        SceneManager.LoadScene(endScreenScene);
    }
}
