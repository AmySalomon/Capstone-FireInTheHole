using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

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

    private PlayerControls controls;
    private PlayerDeath playerDead;

    private void Awake()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerDash = GetComponentInChildren<Dash>();
        playerShoot = GetComponentInChildren<ShootProjectile>();
        playerCharge = GetComponentInChildren<scr_meleeSwing>();
        playerDead = GetComponent<PlayerDeath>();
        playerPause = GetComponent<PlayerPause>();
        controls = new PlayerControls();
    }

   

    public void InitializePlayer(PlayerConfig pc)
    {
        playerConfig = pc;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        playerSprite = playerConfig.PlayerSprite;
        playerColor = playerConfig.PlayerColor;
        Debug.Log("init");
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
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
        if(obj.action.name == controls.Player1.Pause.name)
        {
            OnPause(obj);
        }
        else
        {
            return;
        }


   
    }

    public void OnMove(CallbackContext context)
    {
        if(PlayerPause.paused) { return; }
        if (playerMovement != null)
        {
            Debug.Log("trying to move");
            playerMovement.MovePlayer(context.ReadValue<Vector2>());
        }
    }

    public void OnShoot(CallbackContext context)
    {
        if (PlayerPause.paused) { return; }

        if (playerShoot != null && context.performed && playerCharge.isCharging == false && playerDash.isDashing == false && playerDead.playerIsDead == false)
        {
            Debug.Log("trying to shoot");
            playerShoot.isTryingToShoot = true;
        }

        if (playerShoot != null && context.canceled)
        {
            Debug.Log("trying to shoot");
            playerShoot.isTryingToShoot = false;
        }
    }

    public void OnDash(CallbackContext context)
    {
        if (PlayerPause.paused) { return; }

        if (playerDash != null && context.performed && playerCharge.isCharging == false && playerDead.playerIsDead == false)
        {
            Debug.Log("trying to dash");
            playerDash.PressDash();
        }
    }

    public void OnAim(CallbackContext context)
    {
        if (PlayerPause.paused) { return; }

        if (playerAim != null)
        {
            Debug.Log("trying to aim");
            playerAim.IsAiming(context.ReadValue<Vector2>());
            playerSwingAim.IsAiming(context.ReadValue<Vector2>());
        }
    }

    public void HoldSwing(CallbackContext context)
    {
        if (PlayerPause.paused) { return; }

        if (context.performed && playerDead.playerIsDead == false)
        {
            if (playerCharge.isCharging) playerDash.dashChargeBar.gameObject.SetActive(false);
            playerCharge.StartCharging();
            
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
        if (playerPause != null && context.performed && !PlayerPause.paused)
        {
            playerPause.PauseGame(playerConfig.PlayerIndex);
        }
        else if (playerPause != null && context.performed && PlayerPause.paused && PlayerPause.playerPaused == playerConfig.PlayerIndex)
        {
            playerPause.UnpauseGame();
        }
    }

}
