using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class JoinPlayer : MonoBehaviour
{
    private List<PlayerConfig> playerConfigs;

    [SerializeField] private int MinPlayers = 1;

    [HideInInspector] public bool shouldDisableButton1 = false;
    [HideInInspector] public bool shouldDisableButton2 = false;
    [HideInInspector] public bool shouldDisableButton3 = false;
    [HideInInspector] public bool shouldDisableButton4 = false;

    [HideInInspector] public bool shouldIDisableUI = false;

    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private Transform[] flagSpawns;
    [SerializeField] private Transform[] ballSpawns;
    [SerializeField] private Transform[] turretSpawns;
    [SerializeField] private Transform[] DASHSpawns;
    [SerializeField] private Transform[] SWINGSpawns;
    [SerializeField] private Transform[] coverSpawns;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject tutorialFlagPrefab;
    [SerializeField] private GameObject tutorialBallPrefab;
    [SerializeField] private GameObject tutorialTurretPrefab;
    [SerializeField] private GameObject tutorialTextDASHPrefab;
    [SerializeField] private GameObject tutorialTextSWINGPrefab;
    [SerializeField] private GameObject coverPrefab;
    [SerializeField] private GameObject transitionZonePrefab;

    [SerializeField] private GameObject golfTutStuff;
    [SerializeField] private GameObject dashTutStuff;

    public string sceneToGoTo;
    public static JoinPlayer Instance { get; private set; }

    private PlayerInputHandler[] playersActive;
    private TutorialTurret[] turretsActive;
    private Canvas[] canvasesActive;

    private PlayerInputManager inputManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Singleton - Trying to create another instance of singleton -BAD-");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfig>();
        }

        inputManager = GetComponent<PlayerInputManager>();
    }

    public List<PlayerConfig> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerSprite(int index, Sprite sprite)
    {
        playerConfigs[index].PlayerSprite = sprite;
        
    }

    public void SetVictorySprite(int index, Sprite sprite)
    {
        playerConfigs[index].VictorySprite = sprite;

    }
    public void SetThirdPlaceSprite(int index, Sprite sprite)
    {
        playerConfigs[index].ThirdPlaceSprite = sprite;

    }
    public void SetLastPlaceSprite(int index, Sprite sprite)
    {
        playerConfigs[index].LastPlaceSprite = sprite;

    }
    public void SetPlayerColor(int index, Color color)
    {
        playerConfigs[index].PlayerColor = color;

    }

    public void SetScoreboardSprite(int index, Sprite sprite)
    {
        playerConfigs[index].ScoreboardSprite = sprite;

    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;

        //this code adds the player into the scene once theyre readied up, at their spawn point, and disables the hud for character selection
        //these two first ones are parents of their respective tutorial necessities. They will be disabled when that section is over.
        var myDashTutStuff = Instantiate(dashTutStuff, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        myDashTutStuff.GetComponent<LobbyHoleIdentity>().flagNumber = index + 1;
        var myGolfTutStuff = Instantiate(golfTutStuff, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        myGolfTutStuff.GetComponent<LobbyHoleIdentity>().flagNumber = index + 1;
        var player = Instantiate(playerPrefab, playerSpawns[index].position, playerSpawns[index].rotation, gameObject.transform);
        player.GetComponent<PlayerDeath>().setSpawnLocation = playerSpawns[index];
        player.GetComponent<PlayerDeath>().myIndex = index + 1;
        var tutorialFlag = Instantiate(tutorialFlagPrefab, flagSpawns[index].position, flagSpawns[index].rotation, myGolfTutStuff.transform);
        tutorialFlag.GetComponent<LobbyHoleIdentity>().flagNumber = index + 1;
        var tutorialBall = Instantiate(tutorialBallPrefab, ballSpawns[index].position, ballSpawns[index].rotation, myGolfTutStuff.transform);
        player.GetComponentInChildren<PlayerInputHandler>().InitializePlayer(playerConfigs[index]);
        var tutorialTurret = Instantiate(tutorialTurretPrefab, turretSpawns[index].position, turretSpawns[index].rotation, myDashTutStuff.transform);
        var tutorialDASHText = Instantiate(tutorialTextDASHPrefab, DASHSpawns[index].position, DASHSpawns[index].rotation, myDashTutStuff.transform);
        var tutorialSWINGText = Instantiate(tutorialTextSWINGPrefab, SWINGSpawns[index].position, SWINGSpawns[index].rotation, myGolfTutStuff.transform);
        var transitionZone = Instantiate(transitionZonePrefab, flagSpawns[index].position, flagSpawns[index].rotation, gameObject.transform);
        transitionZone.GetComponent<TutorialAdvanceZone>().myIndex = index + 1;
        var tutCover = Instantiate(coverPrefab, coverSpawns[index].position, Quaternion.identity, myGolfTutStuff.transform);
        tutCover.GetComponent<DisappearOvertime>().shouldIDelete = false;
        myGolfTutStuff.transform.position = new Vector3 (500 + index * 10, 500 + index * 10, 0);
        shouldIDisableUI = true;
    }

    //CODE USED TO DESTROY ACTIVE PLAYER OBJECTS (USUALLY WHEN MOVING TO NEW SCENES IN A DIFFERENT WAY THAN GOTOGAMESCENE
    public void DestroyActivePlayers()
    {
        playersActive = GetComponentsInChildren<PlayerInputHandler>();

        inputManager.enabled = false;
        foreach (PlayerInputHandler player in playersActive)
        {
            Destroy(player.gameObject);
        }

        //code added to handle the tutorial turrets not being destroyed when game starts
        turretsActive = GetComponentsInChildren<TutorialTurret>();
        canvasesActive = GetComponentsInChildren<Canvas>();

        foreach (TutorialTurret turret in turretsActive)
        {
            Destroy(turret.gameObject);
        }

        foreach (Canvas canvas in canvasesActive)
        {
            Destroy(canvas.gameObject);
        }
    }
    //CODE USED TO MOVE TO THE LEVEL SCENE WHEN EVERYONE IS READY
    public void GoToGameScene()
    {
        playersActive = GetComponentsInChildren<PlayerInputHandler>();

        //destroy all players before going to the next scene
        foreach (PlayerInputHandler player in playersActive)
        {
            Destroy(player.gameObject);
        }
            
        //goto the next scene
        if (LevelSelectManager.LSManager.chosenLevel != null) SceneManager.LoadScene(LevelSelectManager.LSManager.chosenLevel);
        else SceneManager.LoadScene(sceneToGoTo);
    }
            

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        //Debug.Log("AAAAAH");
        if(!playerConfigs.Exists(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfig(pi));
        }
    }

    public void HandlePlayerLeave(PlayerInput pi)
    {
        //Debug.Log("Player Left " + pi.playerIndex);
        playerConfigs.Remove(new PlayerConfig(pi));
    }
    public void DisableButton(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 1:
                shouldDisableButton1 = true;
                break;
            case 2:
                shouldDisableButton2 = true;
                break;
            case 3:
                shouldDisableButton3 = true;
                break;
            case 4:
                shouldDisableButton4 = true;
                break;
        }
    }

    public void EnableButton(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 1:
                shouldDisableButton1 = false;
                break;
            case 2:
                shouldDisableButton2 = false;
                break;
            case 3:
                shouldDisableButton3 = false;
                break;
            case 4:
                shouldDisableButton4 = false;
                break;
        }
    }

    //once the player moves into the trigger for the TutorialAdvanceZone script, this activates, resetting the tutorial for stage 2.
    public void DisableDashTutorial(int currentIndex)
    {
        var allTutorials = GetComponentsInChildren<LobbyHoleIdentity>();

        foreach (LobbyHoleIdentity i in allTutorials)
        {
            if (i.gameObject.tag == "GolfTut")
            {
                if (i.flagNumber == currentIndex)
                {
                    i.gameObject.transform.position = new Vector3(0, 0, 0);
                    i.gameObject.GetComponentInChildren<DisappearOvertime>().shouldIDelete = true;
                }
            }

            else if (i.gameObject.tag == "DashTut")
            {
                if (i.flagNumber == currentIndex)
                {
                    i.gameObject.SetActive(false);
                }
            }
        }

        

        playersActive = GetComponentsInChildren<PlayerInputHandler>();

        foreach (PlayerInputHandler player in playersActive)
        {
            if (player.GetComponent<PlayerDeath>().myIndex == currentIndex)
            {
                player.GetComponentInChildren<PlayerMovement>().gameObject.transform.position = player.GetComponent<PlayerDeath>().setSpawnLocation.transform.position;
            }
        }



    }
}

public class PlayerConfig
{
    public PlayerConfig(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
        Stats = new PlayerStats();
    }
    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }

    public Sprite PlayerSprite { get; set; }

    public Color PlayerColor { get; set; }

    public Sprite VictorySprite { get; set; }

    public Sprite ThirdPlaceSprite { get; set; }

    public Sprite LastPlaceSprite { get; set; }

    public Sprite ScoreboardSprite { get; set; }
    public bool placed { get; set; }

    public PlayerStats Stats { get; set; }
}

public class PlayerStats
{
    //violence stats
    public int deaths;
    public int kills;
    public int shotsFired;
    public int golfballKills;
    public int selfDestructs;
    public Dictionary<GameObject, float> deathsBy = new Dictionary<GameObject, float>();

    //putting
    public int puttsTaken;
    public int puttsMissed;

    //interactables stats
    public int powerupsGained;
    public int weaponsGained;
    

}
