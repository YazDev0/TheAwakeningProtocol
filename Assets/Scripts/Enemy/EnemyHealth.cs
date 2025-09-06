using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 100;
    int hp;

    void Awake()
    {
        hp = maxHP;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ÊÏãíÑ ÇáÚÏæ ãÈÇÔÑÉ
        Destroy(gameObject);
    }
}