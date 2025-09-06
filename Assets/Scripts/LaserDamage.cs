using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public int damage = 10; // ãŞÏÇÑ ÇáÖÑÑ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // áæ ÇááÇÚÈ ÚäÏå ÓßÑÈÊ Health
            other.GetComponent<PlayerStats>()?.TakeDamage(damage);
        }
    }
}