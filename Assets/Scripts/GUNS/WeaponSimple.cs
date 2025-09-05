using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSimple : MonoBehaviour
{
    public float range = 100f;

    // ���� �������: Raycast �� ��������
    public void Fire()
    {
        var cam = Camera.main;
        if (!cam) { Debug.Log("No Main Camera."); return; }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);
            // ���� ���� ��� ��� �� ���� Health script
            // hit.collider.GetComponent<Health>()?.TakeDamage(10);
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}