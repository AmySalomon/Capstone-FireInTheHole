using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    public static Vector2 PlayerMovement;

    private PlayerInput playerInput;
    private InputAction moveAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];

    }

    private void Update()
    {
        PlayerMovement = moveAction.ReadValue<Vector2>();
        if (PlayerMovement.magnitude < 0.125)
        {
            PlayerMovement = Vector2.zero;
        }
    }
}
