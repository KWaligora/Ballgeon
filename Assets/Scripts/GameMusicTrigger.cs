﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.OnMenuExit();
    }
}
