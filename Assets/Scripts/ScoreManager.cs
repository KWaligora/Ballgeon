using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public HUD InGameUI;

    public void Awake()
    {
        Instance = this;
    }

    public void AddScore(int value)
    {
        InGameUI.Score += value;
    }
}
