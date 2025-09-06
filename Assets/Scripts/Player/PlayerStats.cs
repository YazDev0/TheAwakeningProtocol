using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public HealthBar healthBar;
    public GameOverManage gameOverManager;


    private void Start()
    {
        Time.timeScale = 1f;

        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetSliderMax(maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (healthBar != null)
            healthBar.SetSlider(currentHealth);
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (healthBar != null)
            healthBar.SetSlider(currentHealth);
    }

    private void Die()
    {
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver(currentHealth, maxHealth);
        }
        else
        {
            Debug.LogWarning("GameOverManager Not Connected PlayerStats");
        }


    }
}

