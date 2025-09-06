using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("Settings")]
    public float sensitivity = 200f;
    public Transform playerBody;    // الكبسولة
    public Transform playerCamera;  // الكاميرا (Main Camera)

    [HideInInspector] public bool uiOpen = false; // تتغير من سكربت اللغز

    private float xRotation = 0f;

    void Update()
    {
        // إذا الـUI مفتوح → وقف كل شيء (لا تحرك كاميرا ولا لاعب)
        if (uiOpen) return;

        // وضع اللعب العادي
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // لف يمين/يسار للجسم كامل
        playerBody.Rotate(Vector3.up * mouseX);

        // لف فوق/تحت للكاميرا فقط
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}