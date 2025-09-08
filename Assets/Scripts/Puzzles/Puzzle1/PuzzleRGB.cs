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
    public string safeCode = "123";

    private string codeTextValue = "";
    private bool isAtDoor = false;

    [Header("Audio")]
    public AudioSource audioSource;  // AudioSource لتشغيل الصوت
    public AudioClip errorSound;     // الصوت الذي سيتم تشغيله عند الخطأ

    void Start()
    {
        CodePanel.SetActive(false);
        LockCursor();
    }

    void Update()
    {
        // تحقق من الرمز بعد كل إدخال
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
        CheckCode(); // التحقق من الرمز بعد كل إدخال
    }

    // === التحقق من الرمز ===
    void CheckCode()
    {
        if (codeTextValue != safeCode && codeTextValue.Length == safeCode.Length)
        {
            PlayErrorSound();  // تشغيل الصوت عند الخطأ
            codeTextValue = "";  // إعادة تعيين الرمز بعد الخطأ
        }
    }

    // === تشغيل الصوت عند الرمز الخاطئ ===
    void PlayErrorSound()
    {
        if (audioSource != null && errorSound != null)
        {
            audioSource.PlayOneShot(errorSound);  // تشغيل الصوت عند الخطأ
        }
    }

    // فتح اللوحة
    void OpenPuzzle()
    {
        CodePanel.SetActive(true);
        camLook.uiOpen = true;  // يوقف الكاميرا
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // إغلاق اللوحة
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