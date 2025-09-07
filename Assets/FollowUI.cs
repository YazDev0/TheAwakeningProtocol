using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FollowUI : MonoBehaviour
{
    public Transform target;   // ������ ��� �����
    public Vector3 offset;     // ����� ��� �����

    void LateUpdate()
    {
        if (target == null) return;

        // ��� �������� ��� ����� ������
        transform.position = target.position + offset;

        // ���� �������� ������
        if (Camera.main != null)
            transform.LookAt(Camera.main.transform);
    }
}