using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManage : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text healthText;
    [SerializeField] private Text maxHealthText;




    private void Start()
    {
        // نتأكد إن اللعبة ماشية طبيعي في البداية
        Time.timeScale = 1f;

        // نخفي واجهة القيم أوفر
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void ShowGameOver(float currentHealth, float maxHealth)
    {
        // نُظهر واجهة القيم أوفر
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        // نحدّث النصوص (اختياري)
        if (healthText != null)
            healthText.text = "Health: " + currentHealth.ToString();

        if (maxHealthText != null)
            maxHealthText.text = "Max Health: " + maxHealth.ToString();

        // نوقف الوقت
        Time.timeScale = 0f;

        // نفك الماوس (مؤشر ظاهر وحُرّ الحركة)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        // نرجّع الوقت لوضعه الطبيعي
        Time.timeScale = 1f;

        // نقفّل الماوس داخل اللعبة (يفيد خصوصاً ألعاب الـFPS)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // نعيد تحميل المشهد الحالي
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}