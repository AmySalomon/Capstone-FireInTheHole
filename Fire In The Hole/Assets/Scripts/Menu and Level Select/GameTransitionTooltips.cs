using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTransitionTooltips : MonoBehaviour
{
    //this script advances through the loading screen
    private float timer = 0;

    public GameObject BallTip;
    public GameObject FightTip;
    public GameObject WinTip;

    private void Start()
    {
        JoinPlayer.Instance.DestroyActivePlayers();
        BallTip.SetActive(false);
        FightTip.SetActive(false);
        WinTip.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 6.5)
        {
            JoinPlayer.Instance.GoToGameScene();
        }

        else if (timer >4.5)
        {
            WinTip.SetActive(true);
        }

        else if (timer > 2.5)
        {
            FightTip.SetActive(true);
        }

        else if (timer > 0.5)
        {
            BallTip.SetActive(true);
        }

    }
}
