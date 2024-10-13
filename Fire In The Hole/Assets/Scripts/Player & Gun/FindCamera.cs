using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindCamera : MonoBehaviour
{
    private Camera mainCamera;
    private Canvas myCanvas;
    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myCanvas = GetComponent<Canvas>();
        myCanvas.worldCamera = mainCamera;
    }
}
