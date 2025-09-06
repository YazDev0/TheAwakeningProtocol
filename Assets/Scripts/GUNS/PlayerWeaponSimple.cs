using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSimple : MonoBehaviour
{
    public Transform hand;          // ���� ��� ������ (Empty ���� �������� �� ��������)
    public float pickRadius = 2f;   // ����� ��������
    private WeaponRaycast current;

    void Update()
    {
        // ������ ���� ���� �� E
        if (Input.GetKeyDown(KeyCode.E) && current == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, pickRadius);
            float best = Mathf.Infinity;
            WeaponRaycast target = null;

            foreach (var h in hits)
            {
                if (h.CompareTag("Weapon"))
                {
                    float d = Vector3.Distance(transform.position, h.transform.position);
                    if (d < best)
                    {
                        best = d;
                        target = h.GetComponent<WeaponRaycast>();
                    }
                }
            }

            if (target) Pickup(target);
        }

        // ����� �������
        if (Input.GetMouseButtonDown(0) && current != null)
        {
            current.Fire();
        }

        // ��� ������ �� G
        if (Input.GetKeyDown(KeyCode.G) && current != null)
        {
            Drop();
        }
    }

    void Pickup(WeaponRaycast w)
    {
        current = w;

        // ���� ������ ������ ������ �����
        var rb = w.GetComponent<Rigidbody>();
        if (rb) { rb.isKinematic = true; rb.useGravity = false; }

        var col = w.GetComponent<Collider>();
        if (col) col.enabled = false;

        w.transform.SetParent(hand);
        w.transform.localPosition = Vector3.zero;
        w.transform.localRotation = Quaternion.identity;
    }

    void Drop()
    {
        current.transform.SetParent(null);

        var rb = current.GetComponent<Rigidbody>();
        if (rb) { rb.isKinematic = false; rb.useGravity = true; rb.AddForce(transform.forward * 2f, ForceMode.Impulse); }

        var col = current.GetComponent<Collider>();
        if (col) col.enabled = true;

        current = null;
    }
}
