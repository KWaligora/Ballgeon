using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighscoreDisplay : MonoBehaviour
{
    private Text[] Texts;

    public void Awake()
    {
        Texts = GetComponentsInChildren<Text>();
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        HighscoreSave save = HighscoreManager.Instance.GetHighscoreSave();
        for(int i = 0; i < save.Highscores.Length; i++)
        {
            Highscore highscore = save.Highscores[i];
            Texts[3* i].text = (i + 1).ToString() + ".";
            Texts[3 * i + 1].text = highscore.Score.ToString();
            Texts[3* i + 2].text = highscore.Name;
        }

       
    }
}
