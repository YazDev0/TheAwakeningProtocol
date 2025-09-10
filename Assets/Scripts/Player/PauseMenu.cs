using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;      // Panel ÔİÇİ İíå ÃÒÑÇÑ
    public string mainMenuScene = "MainMenu";

    public static bool IsPaused { get; private set; }

    // äÎÒä ÍÇáÉ ÇáãÄÔÑ ŞÈá ÇáÅíŞÇİ
    private CursorLockMode _prevLockState;
    private bool _prevCursorVisible;
    private bool _cursorStateSaved;

    // ßæÑæÊíä íËÈÊ ÍÇáÉ ÇáãÄÔÑ æåæ ãæŞøİ
    private Coroutine _cursorKeeper;

    void Start()
    {
        if (pausePanel) pausePanel.SetActive(false);
        IsPaused = false;
        // ãÇ äáãÓ ÍÇáÉ ÇáãÄÔÑ åäÇ – Îáå Òí ãÇ åæ ÚäÏß ÈÇááÚÈÉ
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

        // äæŞİ ÇáßæÑæÊíä Çááí íËÈÊ ÇáãÄÔÑ
        if (_cursorKeeper != null)
        {
            StopCoroutine(_cursorKeeper);
            _cursorKeeper = null;
        }

        // äÑÌøÚ ÍÇáÉ ÇáãÄÔÑ ÇáÓÇÈŞÉ
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

        // äÍİÙ ÍÇáÉ ÇáãÄÔÑ ÇáÍÇáíÉ
        _prevLockState = Cursor.lockState;
        _prevCursorVisible = Cursor.visible;
        _cursorStateSaved = true;

        // äæŞİ ÇáæŞÊ æäİß ÇáãÄÔÑ
        Time.timeScale = 0f;
        IsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // ÈÚÖ ÇáÓßÑÈÊÇÊ ÊŞİøá ÇáãÄÔÑ ßá İÑíãº ÎáøäÇ äËÈøÊ İßøå Øæá ÇáÊæŞíİ
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

            yield return null; // ÇäÊÙÑ İÑíã æÇÑÇÌÚ
        }
    }

    // Buttons
    public void OpenSettingsInPause() { /* ÇİÊÍ Panel ÅÚÏÇÏÇÊ áæ ÊÈÛì */ }

    public void BackToMainMenu()
    {
        // ÑÌøÚ ÇáæŞÊ
        Time.timeScale = 1f;
        IsPaused = false;

        if (_cursorKeeper != null)
        {
            StopCoroutine(_cursorKeeper);
            _cursorKeeper = null;
        }

        // İí ÇáŞÇÆãÉ ÇáÑÆíÓíÉ äÎáøí ÇáãÄÔÑ ÙÇåÑ æãÊÍÑÑ
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(mainMenuScene);
    }
}