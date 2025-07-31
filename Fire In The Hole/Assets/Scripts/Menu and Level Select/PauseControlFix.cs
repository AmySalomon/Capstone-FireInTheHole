using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseControlFix : MonoBehaviour
{
    public GameObject initialButton;

    // Start is called before the first frame update
    void Start()
    {
        var players = FindObjectsOfType<PlayerInput>();
        foreach (PlayerInput input in players)
        {
            input.GetComponent<MultiplayerEventSystem>().firstSelectedGameObject = initialButton;
            input.GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(initialButton);
            input.GetComponent<MultiplayerEventSystem>().enabled = true;
            
            input.GetComponent<InputSystemUIInputModule>().actionsAsset = input.GetComponent<PlayerInput>().actions;
            input.uiInputModule = input.GetComponent<InputSystemUIInputModule>();
            input.GetComponent<InputSystemUIInputModule>().enabled = true;
            
        }
    }


}
