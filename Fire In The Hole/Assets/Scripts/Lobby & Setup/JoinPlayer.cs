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
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject tutorialFlagPrefab;
    [SerializeField] private GameObject tutorialBallPrefab;

    public string sceneToGoTo;
    public static JoinPlayer Instance { get; private set; }

    private PlayerInputHandler[] playersActive;

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

    public void SetPlayerColor(int index, Color color)
    {
        playerConfigs[index].PlayerColor = color;

    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;

        //this code adds the player into the scene once theyre readied up, at their spawn point, and disables the hud for character selection
        var player = Instantiate(playerPrefab, playerSpawns[index].position, playerSpawns[index].rotation, gameObject.transform);
        var tutorialFlag = Instantiate(tutorialFlagPrefab, flagSpawns[index].position, flagSpawns[index].rotation, gameObject.transform);
        tutorialFlag.GetComponent<LobbyHoleIdentity>().flagNumber = index + 1;
        var tutorialBall = Instantiate(tutorialBallPrefab, ballSpawns[index].position, ballSpawns[index].rotation, gameObject.transform);
        player.GetComponentInChildren<PlayerInputHandler>().InitializePlayer(playerConfigs[index]);
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
        Debug.Log("AAAAAH");
        if(!playerConfigs.Exists(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfig(pi));
        }
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
}

public class PlayerConfig
{
    public PlayerConfig(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }

    public Sprite PlayerSprite { get; set; }

    public Color PlayerColor { get; set; }
}
