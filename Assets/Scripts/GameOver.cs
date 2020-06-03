using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnNewGame()
    {
        Enemys.ResetStaticVariables();
        SceneManager.LoadScene("lvl1");
    }

    public void OnMainMenu()
    {
        Enemys.ResetStaticVariables();
        SceneManager.LoadScene("MainMenu");
    }
}
