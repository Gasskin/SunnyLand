using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject stopUi;
    public AudioSource bg;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1)  ;
    }

    public void PauseUi()
    {
        Time.timeScale = 0f;
        bg.Stop();
        stopUi.SetActive(true);
    }

    public void Continue()
    {
        bg.Play();
        Time.timeScale = 1f;
        stopUi.SetActive(false);
    }
}
