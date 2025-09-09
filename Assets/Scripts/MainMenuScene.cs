using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour
{
    public void STARTGAME()
    {
        // ÅÚÇÏÉ ÖÈØ ÇáæŞÊ ÚÔÇä ÇááÚÈÉ ÊÔÊÛá ÚÇÏí İí ÇáÜ Main Menu
        Time.timeScale = 1f;

        // Íãá ãÔåÏ ÇáÜ Main Menu - ÛíÑ ÇÓã ÇáãÔåÏ ÍÓÈ ÇÓãß
        SceneManager.LoadScene("SampleScene");
    }
}