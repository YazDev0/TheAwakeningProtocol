using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PuzzleRGB : MonoBehaviour
{
    public GameObject CodePanel;
    public GameObject laser;
    public CameraLook camLook; // اسحب هنا الكاميرا اللي عليها CameraLook
    public string safeCode = "1234";

    private string codeTextValue = "";
    private bool isAtDoor = false;

    void Start()
    {
        CodePanel.SetActive(false);
        LockCursor();
    }

    void Update()
    {
        if (codeTextValue == safeCode)
        {
            Destroy(laser);
            ClosePuzzle();
            codeTextValue = "";
        }

        if (Input.GetKeyDown(KeyCode.E) && isAtDoor)
        {
            OpenPuzzle();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && CodePanel.activeSelf)
        {
            ClosePuzzle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isAtDoor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAtDoor = false;
            ClosePuzzle();
        }
    }

    public void addDigit(string digit)
    {
        codeTextValue += digit;
    }

    // === هنا تحط الدالتين ===
    void OpenPuzzle()
    {
        CodePanel.SetActive(true);
        camLook.uiOpen = true;  // يوقف الكاميرا
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ClosePuzzle()
    {
        CodePanel.SetActive(false);
        camLook.uiOpen = false; // يرجع يشغل الكاميرا
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}