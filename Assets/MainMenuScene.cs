using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour
{
    public void STARTGAME()
    {
        // ����� ��� ����� ���� ������ ����� ���� �� ��� Main Menu
        Time.timeScale = 1f;

        // ��� ���� ��� Main Menu - ��� ��� ������ ��� ����
        SceneManager.LoadScene("SampleScene");
    }
}