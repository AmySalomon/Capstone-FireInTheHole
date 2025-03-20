using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject portalA, portalB;
    //Portal Position Change variables
    public float portalTimerMin = 5, portalTimerMax = 5, portalTimerCurrent;
    public int portalLocationChosen;
    public List<GameObject> portalALocations = new List<GameObject>();
    public List<GameObject> portalBLocations = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        ResetPortalTimer();
    }

    // Update is called once per frame
    void Update()
    {
        portalTimerCurrent-= Time.deltaTime;
        if (portalTimerCurrent <= 0) { PortalPositionChange(); }
    }
    public void ResetPortalTimer()
    {
        portalTimerCurrent = Random.Range(portalTimerMin, portalTimerMax);
        portalLocationChosen = Random.Range(0, portalALocations.Count);
    }

    public void PortalPositionChange()
    {
        portalA.transform.position = portalALocations[portalLocationChosen].transform.position;
        portalB.transform.position = portalBLocations[portalLocationChosen].transform.position;
        ResetPortalTimer();
    }
}
