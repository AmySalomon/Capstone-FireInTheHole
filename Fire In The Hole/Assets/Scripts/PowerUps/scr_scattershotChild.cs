using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_scattershotChild : MonoBehaviour
{
    public bool active;
    public GameObject golfball;
    public GameObject player;
    //public scr_PU_scattershot scrScattershot;
    public float spawnOffset = 0.5f;

    void Start()
    {
        active = true;
    }

    void Update()
    {
        HitCheck(player);
    }

    public void HitCheck(GameObject player)
    {
        var MeleeSwing = player.GetComponent<scr_meleeSwing>();
        if (MeleeSwing != null)
        {
            if (MeleeSwing.rb != null && active == true)
            {
                Vector2 originalPosition = MeleeSwing.rb.transform.position;
                Vector2 perpendicular = Vector2.Perpendicular(MeleeSwing.forceDirection).normalized * spawnOffset;
                Vector2 leftSpawnPosition = originalPosition - perpendicular;
                Vector2 rightSpawnPosition = originalPosition + perpendicular;

                GameObject Rgolfball;
                GameObject Lgolfball;

                Rgolfball = Instantiate(golfball, rightSpawnPosition, Quaternion.identity);
                Lgolfball = Instantiate(golfball, leftSpawnPosition, Quaternion.identity);

                Rigidbody2D rightRb = Rgolfball.GetComponent<Rigidbody2D>();
                Rigidbody2D leftRb = Lgolfball.GetComponent<Rigidbody2D>();

                float dynamicForce = MeleeSwing.currentSwingForce;

                float angleVariation = 10f; 

                Vector2 rightForceDirection = Quaternion.Euler(0, 0, angleVariation) * MeleeSwing.forceDirection;

                Vector2 leftForceDirection = Quaternion.Euler(0, 0, -angleVariation) * MeleeSwing.forceDirection;


                if (rightRb != null)
                {
                    rightRb.AddForce(rightForceDirection.normalized * dynamicForce / 2);
                }

                if (leftRb != null)
                {
                    leftRb.AddForce(leftForceDirection.normalized * dynamicForce / 2);
                }
                active = false;
                Debug.Log("Scattershot!");
            }

        }
    }
}
