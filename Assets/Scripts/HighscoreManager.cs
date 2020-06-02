using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighscoreManager : MonoBehaviour
{
    HighscoreSave highscoreSave;

    public static HighscoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        highscoreSave = HighscoreSave.GetSavedOrDefault();
    }

    public bool IsHighscore(int score)
    {
       return score > highscoreSave.Highscores.Select(h => h.Score).Min();
    }

    public void CommitHighscore( Highscore highscore )
    {
        highscoreSave.Highscores[highscoreSave.Highscores.Length - 1] = highscore;
        highscoreSave.Highscores = highscoreSave.Highscores.OrderByDescending(h => h.Score).ToArray();
    }

    public string GetHighscoreString()
    {
        return highscoreSave.ToString();
    }

    //Save the highscores on quit
    private void OnApplicationQuit()
    {
        HighscoreSave.Save(highscoreSave);
    }
  

}
