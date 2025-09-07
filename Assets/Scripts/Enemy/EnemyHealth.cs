using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    private int hp;

    [Header("UI")]
    public GameObject healthCanvas; // �������� ��� �����
    public Slider healthBar;        // ���� ���� (Slider)

    void Awake()
    {
        hp = maxHP;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = maxHP;
        }

        // ���� �������� �� �������
        if (healthCanvas != null)
            healthCanvas.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp < 0) hp = 0;

        // ���� �������� ��� ��� ����
        if (healthCanvas != null && !healthCanvas.activeSelf)
            healthCanvas.SetActive(true);

        // ���� ������
        if (healthBar != null)
            healthBar.value = hp;

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " ���!");
        Destroy(gameObject); // ���� �����
    }
}