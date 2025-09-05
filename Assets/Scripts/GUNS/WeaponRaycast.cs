using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : MonoBehaviour
{
    public float range = 100f;         // ��� ������
    public int damage = 10;            // ����� �����
    public Transform muzzle;           // ���� ���� ������ (���� ���� ������)
    public ParticleSystem muzzleFlash; // �������: ���� �����

    public void Fire()
    {
        // ���� ������
        if (muzzleFlash) muzzleFlash.Play();

        // ���� ������ �� �������� �������� (FPS ����� ���) �� �� muzzle
        Transform origin = Camera.main != null ? Camera.main.transform : muzzle;

        if (origin == null)
        {
            Debug.LogWarning("�� ���� Camera.main �� Muzzle ����");
            return;
        }

        Ray ray = new Ray(origin.position, origin.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // �� ��� ����� ����� Health
            // hit.collider.GetComponent<Health>()?.TakeDamage(damage);

            // �� ���� ����� ����� ������ (����)
            // Destroy(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}