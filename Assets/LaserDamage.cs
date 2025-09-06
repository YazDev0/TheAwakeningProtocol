using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public int damage = 10; // ����� �����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �� ������ ���� ����� Health
            other.GetComponent<PlayerStats>()?.TakeDamage(damage);
        }
    }
}