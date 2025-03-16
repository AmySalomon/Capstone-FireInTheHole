using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_permaShield : MonoBehaviour
{
    public GameObject player;
    public PlayerDeath playerDeath;
    public bool shieldActive = false;
    public float shieldChargeTime = 5f; //in seconds
    private bool isRecharging = false;

    void Start()
    {
       //playerDeath = GetComponent<PlayerDeath>();
        shieldActive = true;
        playerDeath.shieldActive = shieldActive;
    }


    public void DisableShieldTemporarily()
    {
        playerDeath.shieldActive = false;
        StartCoroutine(RechargeShield());
    }

    private IEnumerator RechargeShield()
    {
        isRecharging = true;
        yield return new WaitForSeconds(shieldChargeTime);
        playerDeath.shieldActive = true;
        isRecharging = false;
    }
}
