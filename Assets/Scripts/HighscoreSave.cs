using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
            new Highscore{Name = "AAA", Score = 10000 },
            new Highscore{Name = "BBB", Score = 9000 },
            new Highscore{Name = "CCC", Score = 8000 },
            new Highscore{Name = "DDD", Score = 7000 },
            new Highscore{Name = "EEE", Score = 6000 },
            new Highscore{Name = "FFF", Score = 5000 },
            new Highscore{Name = "GGG", Score = 4000 },
            new Highscore{Name = "HHH", Score = 3000 },
            new Highscore{Name = "III", Score = 2000 },
            new Highscore{Name = "JJJ", Score = 1000 }
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
        File.WriteAllText(SavePath, JsonUtility.ToJson(ScoresToSave, true));
    }
}

[System.Serializable]
public class Highscore
{
    public string Name;
    public int Score;
}
