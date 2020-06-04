using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighscoreSave
{
    [SerializeField]
    public Highscore [] Highscores;

    [System.NonSerialized]
    public static string SavePath = Application.dataPath + "\\Save.json";

    public HighscoreSave()
    {
        Highscores = new Highscore[]
        { 
            new Highscore{Name = "aaa", Score = 10000 },
            new Highscore{Name = "bbb", Score = 9000 },
            new Highscore{Name = "ccc", Score = 8000 },
            new Highscore{Name = "ddd", Score = 7000 },
            new Highscore{Name = "eee", Score = 6000 },
            new Highscore{Name = "fff", Score = 5000 },
            new Highscore{Name = "ggg", Score = 4000 },
            new Highscore{Name = "hhh", Score = 3000 },
            new Highscore{Name = "iii", Score = 2000 },
            new Highscore{Name = "jjj", Score = 1000 }
        };
    }
    
    //If the save file exists, loads the highscores - else they have their defaults;
    public static HighscoreSave GetSavedOrDefault()
    {
        if (!File.Exists(SavePath))
            return new HighscoreSave();

        return JsonUtility.FromJson<HighscoreSave>(File.ReadAllText(SavePath));
    }

    public static void Save(HighscoreSave ScoresToSave)
    {
        if (ScoresToSave == null || ScoresToSave.Highscores.Length == 0)
            return;
        File.WriteAllText(SavePath, JsonUtility.ToJson(ScoresToSave, true));
    }
}

[System.Serializable]
public class Highscore
{
    public string Name;
    public int Score;
    public new string ToString() => $"{Score,15} {Name,15}";

}
