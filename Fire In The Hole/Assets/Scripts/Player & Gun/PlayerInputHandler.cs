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
    private PointAtVector playerAim;
    [HideInInspector] public Sprite playerSprite;

    private PlayerControls controls;

    private void Awake()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerDash = GetComponentInChildren<Dash>();
        playerShoot = GetComponentInChildren<ShootProjectile>();
        playerAim = GetComponentInChildren<PointAtVector>();
        controls = new PlayerControls();
    }

    public void InitializePlayer(PlayerConfig pc)
    {
        playerConfig = pc;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        playerSprite = playerConfig.PlayerSprite;
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
        else
        {
            return;
        }
    }

    public void OnMove(CallbackContext context)
    {
        if (playerMovement != null)
        {
            Debug.Log("trying to move");
            playerMovement.MovePlayer(context.ReadValue<Vector2>());
        }
    }

    public void OnShoot(CallbackContext context)
    {
        if (playerShoot != null)
        {
            Debug.Log("trying to shoot");
            playerShoot.ShootAction();
        }
    }

    public void OnDash(CallbackContext context)
    {
        if (playerDash != null)
        {
            Debug.Log("trying to dash");
            playerDash.PressDash();
        }
    }

    public void OnAim(CallbackContext context)
    {
        if (playerAim != null)
        {
            Debug.Log("trying to aim");
            playerAim.IsAiming(context.ReadValue<Vector2>());
        }
    }
}