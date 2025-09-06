using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("Settings")]
    public float sensitivity = 200f;   // ”—⁄… «·„«Ê”
    public Transform playerBody;       // Ã”„ «··«⁄» («·ﬂ»”Ê·…)
    public Transform playerCamera;     // «·ﬂ«„Ì—« («·”·«Õ ·«“„ ÌﬂÊ‰ Child œ«Œ·Â«)

    private float xRotation = 0f;

    void Start()
    {
     //   Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // ·› «·Ã”„ Ì„Ì‰/Ì”«—
        playerBody.Rotate(Vector3.up * mouseX);

        // ·› «·ﬂ«„Ì—« (Ê„⁄Â« «·”·«Õ) ›Êﬁ/ Õ 
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}