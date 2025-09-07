using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : MonoBehaviour
{
    [Header("Settings")]
    public float range = 100f;        // ��� ������
    public int damage = 10;           // ����� �����

    [Header("References")]
    public Transform muzzle;          // ���� ���� ������ (Empty ���� ������)
    public LineRenderer line;         // �� ���� ������/�������

    [Header("Line Options")]
    public float lineDuration = 0.05f;   // ��� ���� ����
    public Vector3 lineOffset = Vector3.zero; // ����� ���� ����� ������

    public void Fire()
    {
        if (muzzle == null || line == null)
        {
            Debug.LogWarning("��� muzzle � line �� Inspector");
            return;
        }

        // Raycast �� �������� (���� ���� ��� ����� �����)
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward);
        Vector3 hitPoint = cam.position + cam.forward * range;

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            hitPoint = hit.point;
            Debug.Log("Hit: " + hit.collider.name);

            // ����: �� ����� ���� Health
             hit.collider.GetComponent<EnemyStats>()?.TakeDamage(damage);
        }

        // ���� �� �� muzzle+offset ��� ���� ������
        Vector3 startPoint = muzzle.position + muzzle.TransformDirection(lineOffset);
        StartCoroutine(ShowLine(startPoint, hitPoint));
    }

    private IEnumerator ShowLine(Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.enabled = true;

        yield return new WaitForSeconds(lineDuration);

        line.enabled = false;
    }
}