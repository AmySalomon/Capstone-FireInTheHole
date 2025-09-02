using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using static UnityEngine.InputSystem.InputAction;
using XInputDotNetPure;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfig playerConfig;
    private PlayerMovement playerMovement;
    private Dash playerDash;
    private ShootProjectile playerShoot;
    public PointAtVector playerAim;
    public PointAtVector playerSwingAim;
    private scr_meleeSwing playerCharge;
    private PlayerPause playerPause;
    [HideInInspector] public Sprite playerSprite;
    [HideInInspector] public Color playerColor;
    public PlayerIndex myIndex;
    private PlayerControls controls;
    private PlayerDeath playerDead;
    private InputDevice device;

    [HideInInspector] public int myInputIndex;

    private Gamepad myGamepad;

    private Vector2 playerDirection;

    public float rumbleTime = 0;
    public float rumbleAmount = 0.1f;
    public bool rumbling = false;

    private void Awake()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerDash = GetComponentInChildren<Dash>();
        playerShoot = GetComponentInChildren<ShootProjectile>();
        playerCharge = GetComponentInChildren<scr_meleeSwing>();
        playerDead = GetComponent<PlayerDeath>();
        playerPause = GetComponent<PlayerPause>();
        controls = new PlayerControls();
        //AssignPlayerIndex();

    }

    private void Update()
    {
        //the system used to dictate how much rumble and for how long for this player. 
        //i downloaded an asset pack called XinputDotNetPure for this following GamePad.SetVibration method, and it works (but only for the first index, and I have no idea why.)
        //for example, if the controller player is player 2, and the MnK player is player 1, then anything player 1 does will trigger the controller's rumble.
        //if controller player is player 1, then it works fine. i haven't tested multiple controllers yet, but this needs fixing for sure.

        //for now, it will be commented out.
        /*if (rumbleTime > 0)
        {
            rumbleTime -= Time.deltaTime;
            GamePad.SetVibration((PlayerIndex)myInputIndex, rumbleAmount, rumbleAmount);
        }
        else
        {
            GamePad.SetVibration((PlayerIndex)myInputIndex, 0, 0);
        }*/

        if (device is Gamepad)
        {
            playerAim.InputDevice = true;
        }
        else if (device is Mouse)
        {
            playerAim.InputDevice = false;
        }
    }

    public void InitializePlayer(PlayerConfig pc)
    {
        playerConfig = pc;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        playerSprite = playerConfig.PlayerSprite;
        playerColor = playerConfig.PlayerColor;
        Debug.Log("init");
        AssignPlayerIndex();
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        device = obj.control.device; //Assigns control type to device (keyboard or controller)



        if (obj.action.name == controls.Player1.Move.name)
        {
            OnMove(obj);
        }
        if (obj.action.name == controls.Player1.Shoot.name)
        {
            OnShoot(obj);
        }
        if (obj.action.name == controls.Player1.Dash.name)
        {
            OnDash(obj);
        }
        if (obj.action.name == controls.Player1.Aim.name)
        {
            OnAim(obj);
        }
        if (obj.action.name == controls.Player1.MeleeSwing.name)
        {
            HoldSwing(obj);
        }
        if (obj.action.name == controls.Player1.Pause.name)
        {
            OnPause(obj);
        }
        if (obj.action.name == controls.Player1.Reload.name)
        {
            OnReload(obj);
        }
        if (obj.action.name == controls.Menus.Cancel.name)
        {
            OnCancelMenu(obj);
        }
        else
        {
            return;
        }



    }

    public void OnMove(CallbackContext context)
    {
        playerDirection = context.ReadValue<Vector2>();
        if (PlayerPause.paused) { return; }
        if (playerMovement != null)
        {
            Debug.Log("trying to move");
            playerMovement.MovePlayer(context.ReadValue<Vector2>());
            playerDead.MoveRespawnIndicator(context.ReadValue<Vector2>());
        }
    }

    public void OnShoot(CallbackContext context)
    {
        if (PlayerPause.paused) { return; }

        if (playerShoot != null && context.performed && playerCharge.isCharging == false && playerDash.isDashing == false && playerDead.playerIsDead == false)
        {
            //Debug.Log("trying to shoot");
            playerShoot.isTryingToShoot = true;
        }

        if (playerShoot != null && context.canceled)
        {
            //Debug.Log("trying to shoot");
            playerShoot.isTryingToShoot = false;
        }
    }

    public void OnDash(CallbackContext context)
    {
        if (PlayerPause.paused) { return; }

        if (playerDash != null && context.performed && playerCharge.isCharging == false && playerDead.playerIsDead == false && playerDirection.magnitude >= 0.1f)
        {
            Debug.Log("trying to dash");
            playerDash.PressDash();
            //to stop player from holding shoot while dashing
            playerShoot.isTryingToShoot = false;
        }
    }

    public void OnAim(CallbackContext context)
    {
        if (PlayerPause.paused) { return; }

        if (playerAim != null)
        {
            if (device is Gamepad)
            {
                // Debug.Log("trying to aim Gamepad");
                playerAim.IsAiming(context.ReadValue<Vector2>());
                playerAim.InputDevice = true;
                playerSwingAim.IsAiming(context.ReadValue<Vector2>());
                playerSwingAim.InputDevice = true;
            }
            else if (device is Mouse)
            {
                // Debug.Log("trying to aim Mouse");
                playerAim.IsAiming(context.ReadValue<Vector2>());
                playerAim.InputDevice = false;
                playerSwingAim.IsAiming(context.ReadValue<Vector2>());
                playerSwingAim.InputDevice = false;
            }
        }
    }

    public void HoldSwing(CallbackContext context)
    {
        if (PlayerPause.paused) { return; }

        if (context.performed && playerDead.playerIsDead == false)
        {
            if (playerCharge.isCharging) playerDash.dashChargeBar.gameObject.SetActive(false);
            playerCharge.StartCharging();
            //to stop player from holding shoot to shoot during swing charge
            playerShoot.isTryingToShoot = false;

        }

        if (context.canceled)
        {
            if (playerCharge.isCharging)
            {
                playerDash.dashChargeBar.gameObject.SetActive(true);
                StartCoroutine(playerCharge.Swing());
            }
        }
    }

    public void OnPause(CallbackContext context)
    {
        if (!PauseMenu.functional) { return; }
        if (playerPause != null && context.performed && !PlayerPause.paused && IntroFlyBy.gameStarted)
        {
            playerPause.PauseGame(playerConfig.PlayerIndex);
        }
        else if (playerPause != null && context.performed && PlayerPause.paused && IntroFlyBy.gameStarted)
        {
            playerPause.UnpauseGame();
        }
    }

    public void OnReload(CallbackContext context)
    {
        if (PlayerPause.paused) { return; }

        if (playerShoot != null && context.performed && playerCharge.isCharging == false && playerDash.isDashing == false && playerDead.playerIsDead == false && playerShoot.ammoCurrent != playerShoot.ammoMax)
        {
            Debug.Log("trying to reload");
            playerShoot.ManualReloadCheck();
        }

    }

    public IEnumerator StartRumble(float rumbleValue, float rumbleTimer) //make the controller vibrate for rumbleTime seconds, then stop
    {
        Debug.Log("trying to rumble controller " + myIndex);
        if (device is Mouse)
        {
            Debug.Log("This is a Mouse " + myIndex);
            yield break;
        }
        rumbleAmount = rumbleValue;
        rumbleTime = rumbleTimer;
        if (OptionsMenuManager.noVibrate == false)
        {
            rumbling = true;
            GamePad.SetVibration(myIndex, 65535 * rumbleAmount, 65535 * rumbleAmount); //65535 is the max amount of rumble, rumble amount is taking a percentage of that
            yield return new WaitForSeconds(rumbleTime);
            Debug.Log(" done rumble controller " + myIndex);
            GamePad.SetVibration(myIndex, 0, 0);
            rumbling = false;
        }
    }

    public void RumbleCheck(float rumbleValue, float rumbleTimer)
    {
        if (device is Mouse)
        {
            Debug.Log("This is a Mouse " + myIndex);
            return;
        }

        if (rumbling) //if vibrating already, stop the old one and replace with new vibration
        {
            StopCoroutine(nameof(StartRumble));
        }
        StartCoroutine(StartRumble(rumbleValue, rumbleTimer));

    }

    public void OnCancelMenu(CallbackContext context)
    {

    }
    //PlayerIndex uses enums of One, Two, Three, Four. Our Indexing starts at 0, so we must convert it
    public void AssignPlayerIndex()
    {
        switch (playerConfig.PlayerIndex)
        {
            case 0:
                myIndex = (PlayerIndex)0; break;
            case 1:
                myIndex = (PlayerIndex)1; break;
            case 2:
                myIndex = (PlayerIndex)2; break;
            case 3:
                myIndex = (PlayerIndex)3; break;
        }
        Debug.Log("Player " + playerConfig.PlayerIndex + " is " + myIndex.ToString());
    }
}
