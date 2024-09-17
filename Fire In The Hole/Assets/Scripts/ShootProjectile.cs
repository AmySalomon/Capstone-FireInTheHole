using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShootProjectile : MonoBehaviour
{
    //gets bullet prefab
    public Rigidbody2D bullet;
    //gets barrel position
    public Transform barrelEnd;
    //timer
    private float timer;
    //the launch force of the bullet being shot
    public float launchForce = -1200f;

    public float shootDelay;

    [SerializeField] private InputActionReference shoot;

    private void OnEnable()
    {
        shoot.action.performed += ShootAction;
    }

    private void OnDisable()
    {
        shoot.action.performed -= ShootAction;
    }


    void Update()
    {
        timer += Time.deltaTime;
    }

    private void ShootAction(InputAction.CallbackContext obj)
    {
        if (timer >= shootDelay)
        {
            Debug.Log("shoot");
            Rigidbody2D bulletInstance;
            bulletInstance = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody2D;
            bulletInstance.AddForce(-barrelEnd.up * launchForce);
            timer = 0;
        }
        
    }
}
