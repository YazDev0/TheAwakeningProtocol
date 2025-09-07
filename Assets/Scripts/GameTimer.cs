using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 60f;   // الوقت بالثواني
    private float currentTime;

    [Header("UI")]
    public TMP_Text timerText;          // نص يعرض الوقت
    public GameObject loseScreen;   // شاشة الخسارة (UI Panel)

    private bool gameOver = false;

    void Start()
    {
        currentTime = startTime;

        if (loseScreen != null)
            loseScreen.SetActive(false); // نخفي شاشة الخسارة بالبداية
    }

    void Update()
    {
        if (gameOver) return;

        currentTime -= Time.deltaTime;
        if (currentTime < 0) currentTime = 0;

        // تحديث النص
        if (timerText != null)
            timerText.text = Mathf.Ceil(currentTime).ToString();

        // لو الوقت انتهى
        if (currentTime <= 0 && !gameOver)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;
        Debug.Log("⏰ الوقت انتهى! اللاعب خسر.");

        if (loseScreen != null)
            loseScreen.SetActive(true);

        // إيقاف اللعبة
        Time.timeScale = 0f;
    }

    // زر إعادة اللعبة (من UI Button)
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
