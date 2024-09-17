using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackUI : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform Target;
    // Update is called once per frame
    void Update()
    {
        if (Target)
        {
            //if the target exists, tracks it down and stays on top of it.
            transform.position = PlayerCamera.WorldToScreenPoint(Target.position);
        }
    }
}
