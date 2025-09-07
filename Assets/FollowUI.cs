using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FollowUI : MonoBehaviour
{
    public Transform target;   // ÇáäŞØÉ İæŞ ÇáÚÏæ
    public Vector3 offset;     // ãÓÇİÉ İæŞ ÇáÑÃÓ

    void LateUpdate()
    {
        if (target == null) return;

        // Îáí ÇáßÇäİÇÓ İæŞ ÇáÚÏæ ÈÇáÖÈØ
        transform.position = target.position + offset;

        // æÇÌå ÇáßÇãíÑÇ ÏÇíãğÇ
        if (Camera.main != null)
            transform.LookAt(Camera.main.transform);
    }
}