using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public Camera playerCamera;

    public float lookSpeed;
    public float lookXLimit = 90f;

    float rotationX = 0f;
    float rotationY = 0f;

    float targetRotationX;
    float targetRotationY;

    [HideInInspector] public bool canMove = true;

    [Tooltip("Smoothing factor")]
    public float smoothSpeed = 20f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        #region Handles look
        if (canMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            rotationY += mouseX;
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            targetRotationX = rotationX;
            targetRotationY = rotationY;

            Quaternion targetCameraRotation = Quaternion.Euler(targetRotationX, 0, 0);
            playerCamera.transform.localRotation = Quaternion.Slerp(playerCamera.transform.localRotation, targetCameraRotation, smoothSpeed);

            Quaternion targetPlayerRotation = Quaternion.Euler(0, targetRotationY, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetPlayerRotation, smoothSpeed);
        }
        #endregion
    }
}
