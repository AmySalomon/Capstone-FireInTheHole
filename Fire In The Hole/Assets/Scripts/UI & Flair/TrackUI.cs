using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackUI : MonoBehaviour
{
    [SerializeField] private Transform Target;

    public float xOffset;
    public float yOffset;

    private Vector3 offsetPos;
    // Update is called once per frame

    private void Start()
    {
        offsetPos = new Vector3(xOffset, yOffset);
    }
    void Update()
    {

        if (Target)
        {
            //if the target exists, tracks it down and stays on top of it.
            transform.position = Target.position + offsetPos;
        }
    }
}
