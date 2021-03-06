﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public HUD InGameUI;
    public HighscoreDisplay HighscoreUI;
    public GameOver GameOverUI;
    public HighscorePrompt HighscorePromptUI;
    public BallRespawn respawnReference;
    public deskBroken bossLockReference;
    bool bossRoomUnlocked;
    public int BossUnlockScore;
    public GameObject DeathParticlePrefab;
    public GameObject DeathParticleOrigin;

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
        bossRoomUnlocked = false;
        InGameUI.Score = 0;
        InGameUI.Lives = 3;
        InGameUI.Level = 1;
    }

    public bool IsDead() => InGameUI.Lives <= 0;

    public void OnLifeDown()
    {
        AudioManager.Instance.PlaySound(AudioKey.LossHP);
        AddLives(-1);
        ParticleSpawnManager.Instance.SpawnParticleForPrefab(DeathParticlePrefab, DeathParticleOrigin.transform.position);

        if (IsDead())
        {
            AudioManager.Instance.PlaySound(AudioKey.GameOver);
            HighscoreUI.gameObject.SetActive(true);
            int PlayerScore = InGameUI.Score;
            if (HighscoreManager.Instance.IsHighscore(PlayerScore))
            {
                HighscorePromptUI.PlayerHighscore = PlayerScore;
                HighscorePromptUI.gameObject.SetActive(true);
            }
            else
                GameOverUI.gameObject.SetActive(true);
        }
        else
            respawnReference.RespawnBall();
    }

    public void AddScore(int value)
    {
        InGameUI.Score += value * InGameUI.Level;
        if(!bossRoomUnlocked && InGameUI.Score >= BossUnlockScore)
        {
            bossLockReference.BreakOpen();
            bossRoomUnlocked = true;
        }
    }

    public void AddLives(int value)
    {
        InGameUI.Lives += value;
    }

    public void SetLevel(int value)
    {
        InGameUI.Level = value;
        AudioManager.Instance.PlaySound(AudioKey.LvlUp);
    }
}