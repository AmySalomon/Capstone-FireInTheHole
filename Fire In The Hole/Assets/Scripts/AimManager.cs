using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimManager : MonoBehaviour
{
    public static Vector2 PlayerAim;

    private PlayerInput playerInput;
    private InputAction aimAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        aimAction = playerInput.actions["Aim"];

    }

    private void Update()
    {
        PlayerAim = aimAction.ReadValue<Vector2>();
        if (PlayerAim.magnitude < 0.125)
        {
            PlayerAim = Vector2.zero;
        }
    }
}
