using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public void ShowMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
