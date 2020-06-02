﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnNewGame()
    {
        SceneManager.LoadScene("lvl1");
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
