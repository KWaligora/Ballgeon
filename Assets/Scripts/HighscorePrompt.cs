using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HighscorePrompt : MonoBehaviour
{
    public int PlayerHighscore;
    public InputField UserNameInput;
    public Button CommitButton;
    public HighscoreDisplay HighscoreDisplayUI;

    public void OnCommit()
    {
        string userName = UserNameInput.text;
        Highscore highscore = new Highscore()
        {
            Name = userName.ToLower(),
            Score = PlayerHighscore
        };
        HighscoreManager.Instance.CommitHighscore(highscore);
        HighscoreDisplayUI.Refresh();
        gameObject.SetActive(false);
    }
}
