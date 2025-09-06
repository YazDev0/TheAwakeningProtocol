using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("Settings")]
    public float sensitivity = 200f;   // ���� ������
    public Transform playerBody;       // ��� ������ (��������)
    public Transform playerCamera;     // �������� (������ ���� ���� Child ������)

    private float xRotation = 0f;

    void Start()
    {
     //   Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // �� ����� ����/����
        playerBody.Rotate(Vector3.up * mouseX);

        // �� �������� (����� ������) ���/���
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}