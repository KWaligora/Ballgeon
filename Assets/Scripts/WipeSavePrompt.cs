using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeSavePrompt : MonoBehaviour
{
    public void OnYes()
    {
        HighscoreManager.Instance.ResetHighscore();
        gameObject.SetActive(false);
    }

    public void OnNo()
    {
        gameObject.SetActive(false);
    }
}
