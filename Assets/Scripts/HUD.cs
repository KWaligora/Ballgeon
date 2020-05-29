using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text ScoreText;
    public Text LevelText;
    public Text LivesText;

    private int score;
    private int level;
    private int lives;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            ScoreText.text = $"Score: {value}";
        }
    }

    public int Level
    {
        get => level;
        set
        {
            level = value;
            LevelText.text = $"Level: {value}";
        }
    }

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            LivesText.text = $"Lives: {value}";
        }
    }
}
