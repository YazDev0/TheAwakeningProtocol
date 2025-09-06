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
        Time.timeScale = 1f;

        if (gameOverUI != null)
            gameOverUI.SetActive(false);

   


    }

    public void ShowGameOver(float currentHealth, float maxHealth)
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        if (healthText != null)
            healthText.text = "Health: " + currentHealth.ToString();

        if (maxHealthText != null)
            maxHealthText.text = "Max Health: " + maxHealth.ToString();



        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
 

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
