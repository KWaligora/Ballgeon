using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public HUD InGameUI;
    public BallRespawn respawnReference;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        InGameUI.Score = 0;
        InGameUI.Lives = 3;
        InGameUI.Level = 1;
    }

    public bool IsDead() => InGameUI.Lives <= 0;

    public void OnLifeDown()
    {
        AddLives(-1);
        if (IsDead())
        {
            //TODO highscore logic
            respawnReference.RespawnBall(); //TODO Remove after highscore UI is 
            Initialize(); //TODO Remove after highscore UI is 
        }
        else
            respawnReference.RespawnBall();
    }

    public void AddScore(int value)
    {
        InGameUI.Score += value * InGameUI.Level;
    }

    public void AddLives(int value)
    {
        InGameUI.Lives += value;
    }

    public void SetLevel(int value)
    {
        InGameUI.Level = value;
    }
}