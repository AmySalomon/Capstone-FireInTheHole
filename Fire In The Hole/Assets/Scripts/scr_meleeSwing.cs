using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class scr_meleeSwing : MonoBehaviour
{
    [Header("Golf Swing Settings")]
    public float swingDistance = 5f;
    public float minSwingForce = 500f;
    public float maxSwingForce = 2000f;
    public float chargeRate = 1000f;
    public float swingCooldown = 1f;
    public int playerDamage = 20;

    private AudioSource audioPlayer;
    public AudioClip missedHit;
    public AudioClip weakHit;
    public AudioClip normalHit;
    public AudioClip strongHit;

    public Transform swingPoint;
    public LayerMask interactableLayers;
    public LayerMask playerLayer;//for pvp player layer

    public bool isCharging = false;
    private bool canSwing = true;
    public float currentSwingForce;

    public Vector3 chargeBarOffset = new Vector3(0, 2, 0);
    public Vector2 chargeBarSize = new Vector2(2, 0.25f);

    public PlayerInputHandler myInput;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    public void StartCharging()
    {
        if (canSwing)
        {
            isCharging = true;
            currentSwingForce = minSwingForce;
            StartCoroutine(ChargeSwingForce());
        }
    }

    private IEnumerator ChargeSwingForce()
    {
        while (isCharging)
        {
            currentSwingForce = Mathf.Clamp(currentSwingForce + chargeRate * Time.deltaTime, minSwingForce, maxSwingForce);
            Debug.Log(currentSwingForce);
            yield return null;
        }
    }

    public IEnumerator Swing()
    {
        isCharging = false;
        canSwing = false;

        Debug.Log($"Swing with force: {currentSwingForce}");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(swingPoint.position, swingDistance, Vector2.zero, 0f, interactableLayers);
        foreach (RaycastHit2D hit in hits)
        {
            Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (currentSwingForce < 600) audioPlayer.PlayOneShot(weakHit);
                else if (currentSwingForce < 1500) audioPlayer.PlayOneShot(normalHit);
                else audioPlayer.PlayOneShot(strongHit);


                Vector2 forceDirection = (hit.collider.transform.position - swingPoint.position).normalized;
                rb.AddForce(forceDirection * currentSwingForce);
            }

        }
        if (hits.Length == 0)
        {
            audioPlayer.PlayOneShot(missedHit);
        }
        currentSwingForce = minSwingForce;

        yield return new WaitForSeconds(swingCooldown);
        canSwing = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (swingPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(swingPoint.position, swingDistance);
        }
        DrawChargeBar();
    }

    private void DrawChargeBar()
    {
        Gizmos.color = Color.green;

        float chargeRatio = (currentSwingForce - minSwingForce) / (maxSwingForce - minSwingForce);

        Vector3 barPosition = swingPoint.position + chargeBarOffset;
        Vector3 barSize = new Vector3(chargeBarSize.x * chargeRatio, chargeBarSize.y, 1);

        Gizmos.color = Color.gray;
        Gizmos.DrawCube(barPosition, new Vector3(chargeBarSize.x, chargeBarSize.y, 1));

        Gizmos.color = Color.green;
        Gizmos.DrawCube(barPosition, barSize);
    }
}

