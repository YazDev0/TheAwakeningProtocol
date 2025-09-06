using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinGameManager : MonoBehaviour
{
    public GameObject winPanel;   // UI Panel
    public Button restartButton;  // زر إعادة التشغيل

    [Header("Win Logic")]
    public bool winOnAnyEnemyDeath = true; // لو false يصير الفوز عند قتل كل الأعداء
    private int initialEnemyCount;
    private bool winTriggered = false;

    void Start()
    {
        if (winPanel) winPanel.SetActive(false);
        if (restartButton) restartButton.onClick.AddListener(RestartGame);

        // احسب عدد الأعداء عند البداية
        initialEnemyCount = CountEnemies();
    }

    void Update()
    {
        if (winTriggered) return;

        int current = CountEnemies();

        if (winOnAnyEnemyDeath)
        {
            // أول عدو يموت → فوز
            if (current < initialEnemyCount)
                ShowWinScreen();
        }
        else
        {
            // لو تبغى الفوز عند قتل الكل
            if (current == 0 && initialEnemyCount > 0)
                ShowWinScreen();
        }
    }

    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void ShowWinScreen()
    {
        if (winTriggered) return;
        winTriggered = true;

        Time.timeScale = 0f;
        if (winPanel) winPanel.SetActive(true);
        Debug.Log("You Win!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}