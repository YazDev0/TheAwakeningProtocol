using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : MonoBehaviour
{
    [Header("Settings")]
    public float range = 100f;        // „œÏ «·”·«Õ
    public int damage = 10;           // „ﬁœ«— «·÷——

    [Header("References")]
    public Transform muzzle;          // „ﬂ«‰ Œ—ÊÃ «·ÿ·ﬁ… (Empty √„«„ «·”·«Õ)
    public LineRenderer line;         // Œÿ ·—”„ «··Ì“—/«·—’«’…

    [Header("Line Options")]
    public float lineDuration = 0.05f;   // „œ… ŸÂÊ— «·Œÿ
    public Vector3 lineOffset = Vector3.zero; //  ⁄œÌ· „ﬂ«‰ »œ«Ì… «··Ì“—

    public void Fire()
    {
        if (muzzle == null || line == null)
        {
            Debug.LogWarning("Õœœ muzzle Ê line ›Ì Inspector");
            return;
        }

        // Raycast „‰ «·ﬂ«„Ì—« (⁄‘«‰ Ì—ÊÕ ⁄·Ï « Ã«Â «·‰Ÿ—)
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward);
        Vector3 hitPoint = cam.position + cam.forward * range;

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            hitPoint = hit.point;
            Debug.Log("Hit: " + hit.collider.name);

            // „À«·: ·Ê «·Âœ› ⁄‰œÂ Health
             hit.collider.GetComponent<EnemyStats>()?.TakeDamage(damage);
        }

        // ‰⁄—÷ Œÿ „‰ muzzle+offset ≈·Ï „ﬂ«‰ «·÷—»…
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