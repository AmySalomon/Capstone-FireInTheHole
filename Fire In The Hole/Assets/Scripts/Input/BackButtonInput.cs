using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class BackButtonInput : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggered;
    public InputSystemUIInputModule inputModule;

    private void OnEnable()
    {
        inputModule.cancel.action.performed += OnCancel;
    }

    private void OnDisable()
    {
        inputModule.cancel.action.performed -= OnCancel;

    }
    private void OnCancel(InputAction.CallbackContext context)
    {

        onTriggered.Invoke();
    }
}
