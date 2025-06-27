using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTransitionTooltips : MonoBehaviour
{
    //this script advances through the loading screen
    private float timer = 0;

    public GameObject BallTip;
    //public GameObject FightTip;
    public GameObject WinTip;
    public GameObject VolcanoTip;
    public GameObject PirateTip;
    public GameObject BlacklightTip;
    public GameObject DinosaurTip;

    private void Start()
    {
        JoinPlayer.Instance.DestroyActivePlayers();
        BallTip.SetActive(false);
        //FightTip.SetActive(false);
        WinTip.SetActive(false);
        VolcanoTip.SetActive(false);
        PirateTip.SetActive(false);
        BlacklightTip.SetActive(false);
        DinosaurTip.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 9.5)
        {
            JoinPlayer.Instance.GoToGameScene();
        }

        else if (timer > 6.5) //Tooltip based on upcoming level.
        {
            if (LevelSelectManager.LSManager.chosenLevel == null)
            {
                Debug.Log("[GameTransitionTooltips]: No level was chosen.");
                //Debug.Log(LevelSelectManager.LSManager.chosenLevel);
            }

            else if (LevelSelectManager.LSManager.chosenLevel == "3D Volcano")
            {
                VolcanoTip.SetActive(true);
                //Debug.Log("[GameTransitionTooltips]: Volcano Chosen.");
            }

            else if (LevelSelectManager.LSManager.chosenLevel == "3D Pirate")
            {
                PirateTip.SetActive(true);
                //Debug.Log("[GameTransitionTooltips]: Pirate Chosen.");
            }

            else if (LevelSelectManager.LSManager.chosenLevel == "3D Blacklight")
            {
                BlacklightTip.SetActive(true);
                //Debug.Log("[GameTransitionTooltips]: Blacklight Chosen.");
            }

            else if (LevelSelectManager.LSManager.chosenLevel == "3D Dinosaur")
            {
                DinosaurTip.SetActive(true);
                //Debug.Log("[GameTransitionTooltips]: Dinosaur Chosen.");
            }
        }

        else if (timer >3.5)
        {
            WinTip.SetActive(true);
        }

        else if (timer > 2.5)
        {
            //FightTip.SetActive(true);
        }

        else if (timer > 0.5)
        {
            BallTip.SetActive(true);
        }

    }
}
