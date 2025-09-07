using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public GameObject healthCanvas; // كانفاس فوق العدو
    public Slider healthBar;        // الشريط نفسه

    void Start()
    {
        currentHealth = maxHealth;

        // إعداد الشريط
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }

        // نخفي الكانفاس بالبداية (يظهر أول ما ينضرب)
        if (healthCanvas != null)
            healthCanvas.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        // نعرض الكانفاس عند أول ضرر
        if (healthCanvas != null && !healthCanvas.activeSelf)
            healthCanvas.SetActive(true);

        // نحدّث الشريط
        if (healthBar != null)
            healthBar.value = currentHealth;

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