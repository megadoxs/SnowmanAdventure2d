using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileDataHandler {
    private static string gameDataPath = "GameData.json";
    private static string highScoreDataPath = "HighScoreData.json";

    public static readonly FileDataHandler instance = new();
    
    public GameData LoadGame()
    {
        var fullPath = Path.Combine(Application.persistentDataPath, gameDataPath);

        if (!File.Exists(fullPath))
        {
            return null;
        }

        using (var reader = new StreamReader(fullPath))
        {
            string json = reader.ReadToEnd();
            return JsonUtility.FromJson<GameData>(json);
        }
    }

    public void SaveGame(GameData data)
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, gameDataPath), JsonUtility.ToJson(data, true));
    }

    public void DeleteGameSave()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, gameDataPath));
    }

    public List<StringIntPair> GetHighScores()
    {
        var fullPath = Path.Combine(Application.persistentDataPath, highScoreDataPath);

        if (!File.Exists(fullPath))
        {
            return null;
        }

        using (var reader = new StreamReader(fullPath))
        {
            return JsonUtility.FromJson<StringIntPairList>(reader.ReadToEnd()).stringIntPairs.OrderByDescending(pair => pair.value).ToList();
        }
    }

    public void SaveScore(string username, int value)
    {
        StringIntPair score = new StringIntPair(username, value);
        var fullPath = Path.Combine(Application.persistentDataPath, highScoreDataPath);
        StringIntPairList scoreList;

        if (File.Exists(fullPath))
        {
            using (var reader = new StreamReader(fullPath))
            {
                string json = reader.ReadToEnd();
                scoreList = JsonUtility.FromJson<StringIntPairList>(json);
            }
        }
        else
        {
            scoreList = new StringIntPairList();
        }

        scoreList.stringIntPairs.Add(score);

        using (var writer = new StreamWriter(fullPath, false))
        {
            string json = JsonUtility.ToJson(scoreList, true);
            writer.Write(json);
        }
    }
}