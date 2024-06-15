using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed;
    public float runSpeed;

    [HideInInspector] public bool canRun = true;
    public bool IsRunning = false;
    public KeyCode runningKey = KeyCode.LeftShift;

    float horizontalInput = 0f;
    float verticalInput = 0f;

    Vector3 move;
    Vector3 velocity;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    public bool IsGrounded;
    public bool canMove = true;

    public CharacterController controller;

    private void Update()
    {
        if (canMove)
        {
            IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (IsGrounded && velocity.y <= 0) velocity.y = -2;

            if (canRun) IsRunning = Input.GetKey(runningKey);
            else IsRunning = false;

            float targetSpeed = IsRunning ? runSpeed : speed;

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            move = transform.right * horizontalInput + transform.forward * verticalInput;
            controller.Move(move * targetSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
