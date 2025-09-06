using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;      // Panel ‘›«› ›ÌÂ √“—«—
    public string mainMenuScene = "MainMenu";

    public static bool IsPaused { get; private set; }

    void Start()
    {
        if (pausePanel) pausePanel.SetActive(false);
        IsPaused = false;
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
        if (pausePanel) pausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        if (pausePanel) pausePanel.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    // Buttons
    public void OpenSettingsInPause() { /* «› Õ Panel ≈⁄œ«œ«  ·Ê  »€Ï */ }
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}