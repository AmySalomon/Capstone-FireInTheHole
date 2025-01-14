using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpecialEvent/TideChange")]
public class TideChange : SpecialEvents
{
    public bool currentLowTide;
    GameObject lowTide, highTide;
    public override void InitiateEvent()
    {
        if (currentLowTide)
        {
            ChangeHighTide();
        }
        else
        {
            ChangeLowTide();
        }

    }

    public override void EventSetup(GameObject theManager)
    {
        base.EventSetup(theManager);
        lowTide = GameObject.FindGameObjectWithTag("Low Tide");
        if (lowTide != null)
        {
            Debug.Log("success! lowtide is " + lowTide.name);
        }
        else
        {
            Debug.Log("failed to set lowtide");
        }
        highTide = GameObject.FindGameObjectWithTag("High Tide");
        if (highTide != null)
        {
            Debug.Log("success! hightide is " + highTide.name);
        }
        else
        {
            Debug.Log("failed to set hightide");
        }
        lowTide.SetActive(true);
        highTide.SetActive(false);
        currentLowTide = true;
    }

    void ChangeHighTide()
    {
        lowTide.SetActive(false);
        highTide.SetActive(true);
        currentLowTide = false;
    }

    void ChangeLowTide()
    {
        lowTide.SetActive(true);
        highTide.SetActive(false);
        currentLowTide = true;
    }
}
