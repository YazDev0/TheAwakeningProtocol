using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController Character;
    public float speed = 2f;

    public float gravity = -9.81f;
    public float jumpPower = 2f;
    float Velocity;
    public Transform cameraTransform;

    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        // camera direction
        Vector3 move = cameraTransform.right * Horizontal + cameraTransform.forward * Vertical;
        if (move.magnitude > 1f) move.Normalize();



        if (Input.GetKey(KeyCode.LeftShift))
        {
            move *= speed * 2;
        }
        else
        {
            move *= speed;
        }

        // Gravity
        if (Character.isGrounded)
        {
            Velocity = -1;
            if (Input.GetButtonDown("Jump"))
            {
                Velocity = Mathf.Sqrt(jumpPower * -2f * gravity);
            }
        }

        Velocity += gravity * Time.deltaTime;


        Vector3 finalMove = move;
        finalMove.y = Velocity;

        Character.Move(finalMove * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb != null && !rb.isKinematic)
        {
            Vector3 force = hit.moveDirection * 5f;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}