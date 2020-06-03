using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject CurrentlyInRightSlot;
    public GameObject HighscoreDisplayUI;
    public GameObject CreditsDisplayUI;
    public GameObject WipeSavePromptUI;

    public void OnStart()
    {
        SceneManager.LoadScene("lvl1");
    }

    public void OnHighscore()
    {
        SwapActive(CurrentlyInRightSlot, HighscoreDisplayUI);
    }

    public void OnCredits()
    {
        SwapActive(CurrentlyInRightSlot, CreditsDisplayUI);
    }

    public void OnWipeSave()
    {
        SwapActive(CurrentlyInRightSlot, WipeSavePromptUI);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    //Hides the previous and shows the next
    private void SwapActive(GameObject previous, GameObject next)
    {
        if (previous != null)
            previous.SetActive(false);
        if(next != null)
        {
            CurrentlyInRightSlot = next;
            next.SetActive(true);
        }

    }

}
