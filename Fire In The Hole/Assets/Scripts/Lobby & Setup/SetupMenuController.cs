using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupMenuController : MonoBehaviour
{
    private int PlayerIndex;

    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private GameObject readyPanel;

    [SerializeField] private GameObject menuPanel;

    [SerializeField] private Button readyButton;

    [HideInInspector] public WhatButtonAmI buttonToDisable;

    [SerializeField] private Button characterButton1;
    [SerializeField] private Button characterButton2;
    [SerializeField] private Button characterButton3;
    [SerializeField] private Button characterButton4;

    private RectTransform rectTransform;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        rectTransform = GetComponent<RectTransform>();
        //Debug.Log(pi);
        titleText.SetText("Player " +(pi + 1).ToString());
        switch (pi + 1)
        {
            //this places the menus in the correct positions based on spawn location. may need to update again later.
            //the X axis 1 is .8f, and the Y axis 1 is .75f, in order to have better spacing.
            //the X axis 0 is .2f, and the Y axis 0 is .3f, for spacing reasons as well.
            case 1:
                rectTransform.anchorMin = new Vector2(.2f, .75f);
                rectTransform.anchorMax = new Vector2(.2f, .75f);
                break;

            case 2:
                rectTransform.anchorMin = new Vector2(.8f, .3f);
                rectTransform.anchorMax = new Vector2(.8f, .3f);
                break;

            case 3:
                rectTransform.anchorMin = new Vector2(.8f, .75f);
                rectTransform.anchorMax = new Vector2(.8f, .75f);
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
    }

    public void SetSprite(Sprite sprite)
    {
        if(!inputEnabled) { return; }

        JoinPlayer.Instance.SetPlayerSprite(PlayerIndex, sprite);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void SetVictorySprite(Sprite sprite)
    {
        if (!inputEnabled) { return; }
        JoinPlayer.Instance.SetVictorySprite(PlayerIndex, sprite);

    }
    public void SetColor(string color)
    {
        if (!inputEnabled) { return; }
        ColorUtility.TryParseHtmlString(color, out Color playerColor);
        JoinPlayer.Instance.SetPlayerColor(PlayerIndex, playerColor);

    }

    public void DisableThisButton(Button button)
    {
        if (!inputEnabled) { return; }

        buttonToDisable = button.gameObject.GetComponent<WhatButtonAmI>();
        JoinPlayer.Instance.DisableButton(buttonToDisable.buttonNumber);
        //the joinplayer instance will now trigger a bool, that disables this button in the update function of this class, for all versions of this class.
    }


    public void ReadyPlayer()
    {
        if(!inputEnabled) { return; }

        JoinPlayer.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);

        //this disables the UI panel after selecting character. in future, we will have to make a way to return to this UI panel after selecting a character
        gameObject.SetActive(false);
    }
}
