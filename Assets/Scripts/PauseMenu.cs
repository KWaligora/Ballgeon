using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;

    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    private void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);       
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        Enemys.ResetStaticVariables();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        Enemys.ResetStaticVariables();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
