using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;      // Panel ���� ��� �����
    public string mainMenuScene = "MainMenu";

    public static bool IsPaused { get; private set; }

    // ���� ���� ������ ��� �������
    private CursorLockMode _prevLockState;
    private bool _prevCursorVisible;
    private bool _cursorStateSaved;

    // ������� ���� ���� ������ ��� �����
    private Coroutine _cursorKeeper;

    void Start()
    {
        if (pausePanel) pausePanel.SetActive(false);
        IsPaused = false;
        // �� ���� ���� ������ ��� � ��� �� �� �� ���� �������
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        if (!IsPaused) return;

        if (pausePanel) pausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;

        // ���� ��������� ���� ���� ������
        if (_cursorKeeper != null)
        {
            StopCoroutine(_cursorKeeper);
            _cursorKeeper = null;
        }

        // ����� ���� ������ �������
        if (_cursorStateSaved)
        {
            Cursor.lockState = _prevLockState;
            Cursor.visible = _prevCursorVisible;
        }
    }

    void Pause()
    {
        if (IsPaused) return;

        if (pausePanel) pausePanel.SetActive(true);

        // ���� ���� ������ �������
        _prevLockState = Cursor.lockState;
        _prevCursorVisible = Cursor.visible;
        _cursorStateSaved = true;

        // ���� ����� ���� ������
        Time.timeScale = 0f;
        IsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // ��� ��������� ����� ������ �� ���� ����� ����� ���� ��� �������
        _cursorKeeper = StartCoroutine(ForceCursorWhilePaused());
    }

    IEnumerator ForceCursorWhilePaused()
    {
        while (IsPaused)
        {
            if (Cursor.lockState != CursorLockMode.None)
                Cursor.lockState = CursorLockMode.None;
            if (!Cursor.visible)
                Cursor.visible = true;

            yield return null; // ����� ���� ������
        }
    }

    // Buttons
    public void OpenSettingsInPause() { /* ���� Panel ������� �� ���� */ }

    public void BackToMainMenu()
    {
        // ���� �����
        Time.timeScale = 1f;
        IsPaused = false;

        if (_cursorKeeper != null)
        {
            StopCoroutine(_cursorKeeper);
            _cursorKeeper = null;
        }

        // �� ������� �������� ����� ������ ���� ������
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(mainMenuScene);
    }
}