using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSimple : MonoBehaviour
{
    public Transform hand;          // ⁄Ì¯‰ ⁄Ÿ„…/Empty ›Ì Ìœ «··«⁄»
    public float pickRadius = 2f;   // „”«›… «·«· ﬁ«ÿ
    private WeaponSimple current;

    void Update()
    {
        // «· ﬁÿ √ﬁ—» ”·«Õ »‹ E
        if (Input.GetKeyDown(KeyCode.E) && current == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, pickRadius);
            float best = Mathf.Infinity;
            WeaponSimple target = null;

            foreach (var h in hits)
            {
                if (h.CompareTag("Weapon"))
                {
                    float d = Vector3.Distance(transform.position, h.transform.position);
                    if (d < best)
                    {
                        best = d;
                        target = h.GetComponent<WeaponSimple>();
                    }
                }
            }

            if (target) Pickup(target);
        }

        // «ÿ·«ﬁ »«·”·«Õ
        if (Input.GetMouseButtonDown(0) && current != null)
        {
            current.Fire();
        }

        // —„Ì «·”·«Õ »‹ G («Œ Ì«—Ì)
        if (Input.GetKeyDown(KeyCode.G) && current != null)
        {
            Drop();
        }
    }

    void Pickup(WeaponSimple w)
    {
        current = w;

        // √Êﬁ› ›Ì“Ì«¡ «·”·«Õ Ê«—»ÿÂ »«·Ìœ
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
        // ›ıﬂ «·—»ÿ Ê√⁄œ «·›Ì“Ì«¡
        current.transform.SetParent(null);

        var rb = current.GetComponent<Rigidbody>();
        if (rb) { rb.isKinematic = false; rb.useGravity = true; rb.AddForce(transform.forward * 0.5f, ForceMode.Impulse); }

        var col = current.GetComponent<Collider>();
        if (col) col.enabled = true;

        current = null;
    }
}
