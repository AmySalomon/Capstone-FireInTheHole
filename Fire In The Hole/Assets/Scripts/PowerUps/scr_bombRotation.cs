using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bombRotation : MonoBehaviour
{
    public Rigidbody2D ballRigidbody;
    public Transform Bombprefab;
    public float rotationSpeedMultiplier;

    void Update()
    {
        if (ballRigidbody == null || Bombprefab == null) return;

        Vector2 velocity = ballRigidbody.velocity;
        float speed = velocity.magnitude;

        if (speed > 0.01f)
        {
            Vector3 movementDirection = new Vector3(velocity.x, 0f, velocity.y).normalized;

            Vector3 rotationAxis = Vector3.Cross(movementDirection, Vector3.up);

            float rotationAngle = -speed * rotationSpeedMultiplier * Time.deltaTime;
            Bombprefab.Rotate(rotationAxis, rotationAngle, Space.World);
        }
    }
}