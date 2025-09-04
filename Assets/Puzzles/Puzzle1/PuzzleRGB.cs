using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PuzzleRGB : MonoBehaviour
{
    private bool IsAtDoor = false;

    [SerializeField] private TextMeshProUGUI CodeText;
    private string codeTextValue = "";

    [Header("Settings")]
    public string safeCode = "1234";
    public GameObject CodePanel;
    public GameObject laser;

    [Header("Latch Glow")]
    [SerializeField] private float latchBoost = 0.35f;  // ��� ������� ��� �������

    // ����� ����� ������� ������� ������� ��� �����/������/����� ������
    private readonly Dictionary<Image, Color> originalColors = new Dictionary<Image, Color>();
    private readonly HashSet<Image> latchedImages = new HashSet<Image>();

    void Start()
    {
        if (CodePanel != null) CodePanel.SetActive(false);
    }

    void Update()
    {
        if (CodeText != null) CodeText.text = codeTextValue;

        // ����: ���� ������ + ���� ������ + ���� ����� + ���� ����� �������
        if (codeTextValue == safeCode)
        {
            if (laser != null) Destroy(laser);
            if (CodePanel != null) CodePanel.SetActive(false);
            codeTextValue = "";
            ResetLatched();
        }

        // ����� ��� 4 ����� (��� �����) + ����� �������
        if (codeTextValue.Length >= 4)
        {
            codeTextValue = "";
            ResetLatched();
        }

        // ��� ������ ��� ����� ��� E ������ �� �����
        if (Input.GetKeyDown(KeyCode.E) && IsAtDoor == true)
        {
            if (CodePanel != null) CodePanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsAtDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsAtDoor = false;
            if (CodePanel != null) CodePanel.SetActive(false);
            ResetLatched(); // ��� ����� ������ ����� �������
        }
    }

    // ����� ���� ����� (�� ���� ��������)
    public void addDigit(string digit)
    {
        codeTextValue += digit;
    }

    // ����� + "���� �����" ���� ������� �������� (�� ����� ����� Image)
    public void addDigitLatchedSimple(string digit)
    {
        codeTextValue += digit;

        // ����� ���� ������ �� EventSystem ������ ������
        var go = EventSystem.current != null ? EventSystem.current.currentSelectedGameObject : null;
        if (go != null)
        {
            var img = go.GetComponent<Image>();
            if (img == null) img = go.GetComponentInChildren<Image>();
            if (img != null) LatchImage(img);
        }
        else
        {
            Debug.LogWarning("[PuzzleRGB] �� ��� ������ ��� currentSelectedGameObject. ����� �� ���� EventSystem �� ������.");
        }
    }

    // ����� ������� ��� �� ����
    private void LatchImage(Image img)
    {
        if (img == null) return;

        // ���� ����� ������ ��� �����
        if (!originalColors.ContainsKey(img))
            originalColors[img] = img.color;

        // ��� ��� ���� ������ �� ���� �������
        if (latchedImages.Contains(img)) return;

        img.color = BrightenHSV(img.color, latchBoost);
        latchedImages.Add(img);
    }

    // ����� �� ������� �������� �������
    private void ResetLatched()
    {
        foreach (var kv in originalColors)
        {
            if (kv.Key) kv.Key.color = kv.Value;
        }
        originalColors.Clear();
        latchedImages.Clear();
    }

    // ����� ���� ����HSV (����� ���Value ���)
    private Color BrightenHSV(Color c, float amount)
    {
        Color.RGBToHSV(c, out float h, out float s, out float v);
        v = Mathf.Clamp01(v + amount);
        var outC = Color.HSVToRGB(h, s, v);
        outC.a = c.a;
        return outC;
    }

    // (�������) �� "���"
    public void ClearCode()
    {
        codeTextValue = "";
        ResetLatched();
    }
}
