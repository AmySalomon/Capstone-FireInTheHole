using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using static UnityEngine.InputSystem.InputAction;
using XInputDotNetPure;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using System;

public class SetupMenuController : MonoBehaviour
{
    private int PlayerIndex;

    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private GameObject readyPanel;

    [SerializeField] private GameObject menuPanel;

    [SerializeField] private Button readyButton;

    [HideInInspector] public WhatButtonAmI buttonToDisable;
    [HideInInspector] public GameObject currentWindow;

    [SerializeField] private Button characterButton1;
    [SerializeField] private Button characterButton2;
    [SerializeField] private Button characterButton3;
    [SerializeField] private Button characterButton4;
    [SerializeField] private Button selectedButton;

    private RectTransform rectTransform;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;
    private GameObject currentPanel;
    [SerializeField] private MultiplayerEventSystem multiplayerEventSystem;
    private PlayerInput myInput;
    private InputDevice device;
    [SerializeField] private InputSystemUIInputModule module;
    private PlayerControls controls;

    private void Awake()
    {
        currentPanel = menuPanel;
        multiplayerEventSystem = gameObject.GetComponentInChildren<MultiplayerEventSystem>();
        controls = new PlayerControls();
    }

    private void MyInput_onActionTriggered(CallbackContext obj)
    {
        device = obj.control.device;

        if(obj.action.name == controls.Menus.Cancel.name)
        {
            GoBack(obj);
        }
    }

    public void GoBack(CallbackContext context)
    {
        if (context.performed)
        {
            if (currentPanel == null) { return; }

            if (currentPanel == menuPanel)
            {
                Debug.Log("Lol lmao");
            }

            if (currentPanel == readyPanel)
            {
                multiplayerEventSystem.SetSelectedGameObject(null); //needed so the readybutton is not automatically pressed once a character is picked again
                readyPanel.SetActive(false);
                menuPanel.SetActive(true);
                EnableThisButton(selectedButton);
                currentPanel = menuPanel;
                multiplayerEventSystem.SetSelectedGameObject(selectedButton.gameObject);
                Debug.Log("Player "+myInput.playerIndex+" has cancelled");
            }
        }
    }
    public void SetPlayerIndex(int pi, PlayerInput input)
    {
        PlayerIndex = pi;
        myInput = input;
        myInput.onActionTriggered += MyInput_onActionTriggered;
        rectTransform = GetComponent<RectTransform>();
        //Debug.Log(pi);
        titleText.SetText("Player " +(pi + 1).ToString());
        switch (pi + 1)
        {
            //this places the menus in the correct positions based on spawn location. may need to update again later.

            case 1:
                rectTransform.anchorMin = new Vector2(.2f, .65f);
                rectTransform.anchorMax = new Vector2(.2f, .65f);
                break;

            case 2:
                rectTransform.anchorMin = new Vector2(.8f, .3f);
                rectTransform.anchorMax = new Vector2(.8f, .3f);
                break;

            case 3:
                rectTransform.anchorMin = new Vector2(.8f, .65f);
                rectTransform.anchorMax = new Vector2(.8f, .65f);
                break;
            case 4:
                rectTransform.anchorMin = new Vector2(.2f, .3f);
                rectTransform.anchorMax = new Vector2(.2f, .3f);
                break;
        }
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }

        //singleton variable means only one player can pick each button, so no duplicate characters.
        if (JoinPlayer.Instance.shouldDisableButton1) characterButton1.interactable = false;
        if (JoinPlayer.Instance.shouldDisableButton2) characterButton2.interactable = false;
        if (JoinPlayer.Instance.shouldDisableButton3) characterButton3.interactable = false;
        if (JoinPlayer.Instance.shouldDisableButton4) characterButton4.interactable = false;
        if (!JoinPlayer.Instance.shouldDisableButton1) characterButton1.interactable = true;
        if (!JoinPlayer.Instance.shouldDisableButton2) characterButton2.interactable = true;
        if (!JoinPlayer.Instance.shouldDisableButton3) characterButton3.interactable = true;
        if (!JoinPlayer.Instance.shouldDisableButton4) characterButton4.interactable = true;
    }

    
    public void SetSprite(Sprite sprite)
    {
        if(!inputEnabled) { return; }

        JoinPlayer.Instance.SetPlayerSprite(PlayerIndex, sprite);
        readyPanel.SetActive(true);
        multiplayerEventSystem.SetSelectedGameObject(readyButton.gameObject);
        menuPanel.SetActive(false);
        currentPanel = readyPanel;
    }

    public void SetVictorySprite(Sprite sprite)
    {
        if (!inputEnabled) { return; }
        JoinPlayer.Instance.SetVictorySprite(PlayerIndex, sprite);

    }

    public void SetThirdPlaceSprite(Sprite sprite)
    {
        if (!inputEnabled) { return; }
        JoinPlayer.Instance.SetThirdPlaceSprite(PlayerIndex, sprite);

    }

    public void SetLastPlaceSprite(Sprite sprite)
    {
        if (!inputEnabled) { return; }
        JoinPlayer.Instance.SetLastPlaceSprite(PlayerIndex, sprite);

    }

    public void SetColor(string color)
    {
        if (!inputEnabled) { return; }
        ColorUtility.TryParseHtmlString(color, out Color playerColor);
        JoinPlayer.Instance.SetPlayerColor(PlayerIndex, playerColor);

    }

    public void SetScoreboard(Sprite sprite)
    {
        if (!inputEnabled) { return; }
        JoinPlayer.Instance.SetScoreboardSprite(PlayerIndex, sprite);
    }

    public void DisableThisButton(Button button)
    {
        if (!inputEnabled) { return; }
        buttonToDisable = button.gameObject.GetComponent<WhatButtonAmI>();
        JoinPlayer.Instance.DisableButton(buttonToDisable.buttonNumber);
        //the joinplayer instance will now trigger a bool, that disables this button in the update function of this class, for all versions of this class.
        selectedButton = button;
    }

    public void EnableThisButton(Button button)
    {
        JoinPlayer.Instance.EnableButton(buttonToDisable.buttonNumber);

    }

    public void ReadyPlayer()
    {
        if(!inputEnabled) { return; }

        JoinPlayer.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);

        //All of the following exists so that everyone can control the pause menu on the tutorial lobby
        Destroy(module);
        Destroy(multiplayerEventSystem);

        myInput.uiInputModule = myInput.GetComponent<InputSystemUIInputModule>();
        myInput.GetComponent<InputSystemUIInputModule>().actionsAsset = myInput.GetComponent<PlayerInput>().actions;

        myInput.GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(FindObjectOfType<PauseMenu>().quickFixForTutButton.gameObject);

        gameObject.SetActive(false);
 
    }
}
