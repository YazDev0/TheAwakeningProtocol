using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);

        // بعد ما يموت العدو، نحسب عدد الأعداء المتبقين
        int remaining = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (remaining == 0)
        {
            // كل الأعداء ماتوا → فوز
            WinGameManager win = FindObjectOfType<WinGameManager>();
            if (win != null)
            {
                win.ShowWinScreen();
            }
            else
            {
                Debug.LogWarning("⚠️ WinGameManager not found in scene!");
            }
        }
    }
}