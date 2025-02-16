using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeScrollbar : MonoBehaviour
{
    public GameObject targetScrollbar;

    public void MoveScrollbar(int newPos) //move the object to the new position indicated
    {
        targetScrollbar.transform.localPosition = new Vector3 (0, newPos, 0);
    }
}
