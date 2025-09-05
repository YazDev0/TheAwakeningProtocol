using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : MonoBehaviour
{
    public float range = 100f;              // ��� ������
    public int damage = 10;                 // ����� �����
    public Transform muzzle;                // ���� ���� ������
    public LineRenderer line;               // �� ���� ������
    public float lineDuration = 0.05f;      // �� ��� ���� ����

    public void Fire()
    {
        if (muzzle == null) muzzle = transform;

        Ray ray = new Ray(muzzle.position, muzzle.forward);
        Vector3 endPoint = muzzle.position + muzzle.forward * range;

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            endPoint = hit.point;
            Debug.Log("Hit: " + hit.collider.name);

            // ����: �� ��� ����� ����� Health
            // hit.collider.GetComponent<Health>()?.TakeDamage(damage);
        }

        // ��� ����
        if (line != null)
        {
            StartCoroutine(ShowLine(endPoint));
        }
    }

    private System.Collections.IEnumerator ShowLine(Vector3 endPoint)
    {
        line.SetPosition(0, muzzle.position);
        line.SetPosition(1, endPoint);
        line.enabled = true;

        yield return new WaitForSeconds(lineDuration);

        line.enabled = false;
    }
}