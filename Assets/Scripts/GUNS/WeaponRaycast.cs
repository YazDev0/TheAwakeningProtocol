using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : MonoBehaviour
{
    public float range = 100f;         // ãÏì ÇáÓáÇÍ
    public int damage = 10;            // ãŞÏÇÑ ÇáÖÑÑ
    public Transform muzzle;           // ãßÇä ÎÑæÌ ÇáÔÚÇÚ (ÚÇÏÉ ÃãÇã ÇáÓáÇÍ)
    public ParticleSystem muzzleFlash; // ÇÎÊíÇÑí: İáÇÔ ÅØáÇŞ

    public void Fire()
    {
        // ãÔåÏ ÇáİáÇÔ
        if (muzzleFlash) muzzleFlash.Play();

        // äÈÏÃ ÇáÔÚÇÚ ãä ÇáßÇãíÑÇ ÇáÑÆíÓíÉ (FPS ãäÙæÑ Ãæá) Ãæ ãä muzzle
        Transform origin = Camera.main != null ? Camera.main.transform : muzzle;

        if (origin == null)
        {
            Debug.LogWarning("áÇ íæÌÏ Camera.main Ãæ Muzzle ãÎÕÕ");
            return;
        }

        Ray ray = new Ray(origin.position, origin.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // áæ ÚäÏ ÇáåÏİ ÓßÑÈÊ Health
            // hit.collider.GetComponent<Health>()?.TakeDamage(damage);

            // áæ ÊÈÛì ÊÏãøÑ ÇáåÏİ ãÈÇÔÑÉ (ãËÇá)
            // Destroy(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}