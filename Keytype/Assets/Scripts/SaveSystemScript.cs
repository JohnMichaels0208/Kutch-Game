using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class SaveSystemScript
{
    public const string optionSaveFileName = "/optionsavefile.json";

    public const string progressSaveFileName = "/progresssavefile.json";

    public const string levelSaveFileName = "/levelsavefile.json";

    public static void SaveOption(OptionData optionData)
    {
        string path = Application.persistentDataPath + optionSaveFileName;
        string json = JsonUtility.ToJson(optionData);
        File.WriteAllText(path, json);
    }

    public static OptionData LoadOptionData()
    {
        string path = Application.persistentDataPath + optionSaveFileName;
        if (!File.Exists(path))
        {
            CreateOption();
        }
        string json = File.ReadAllText(path);
        OptionData optionData = JsonUtility.FromJson<OptionData>(json);
        return optionData;
    }

    public static void CreateOption()
    {
        string path = Application.persistentDataPath + optionSaveFileName;
        OptionData optionData = new OptionData(1, 1, true);
        string json = JsonUtility.ToJson(optionData);
        File.WriteAllText(path, json);
    }

    public static void CreateLevel()
    {
        string path = Application.persistentDataPath + levelSaveFileName;
        string json = JsonUtility.ToJson(new LevelSaveClass(new List<LevelData>()));
        File.WriteAllText(path, json);
    }



    public static List<LevelData> LoadLevelDataList()
    {
        string path = Application.persistentDataPath + levelSaveFileName;
        if (!File.Exists(path))
        {
            CreateLevel();
        }
        string json = File.ReadAllText(path);
        List<LevelData> levelDataList = JsonUtility.FromJson<LevelSaveClass>(json).listOfLevels;
        return levelDataList;
    }

    public static int LoadLevelIndexByButtonName(string buttonName)
    {
        List<LevelData> levelDatas = LoadLevelDataList();
        for (int i = 0; i < levelDatas.Count; i++)
        {
            if (levelDatas[i].associatedButtonName == buttonName)
            {
                return i;
            }
        }
        return 0;
    }

    public static int LoadLevelIndexBySceneName(string sceneName)
    {
        List<LevelData> levelDatas = LoadLevelDataList();
        for (int i = 0; i < levelDatas.Count; i++)
        {
            if (levelDatas[i].associatedSceneName == sceneName)
            {
                return i;
            }
        }
        return 0;
    }

    public static void SaveLevelData(int index, LevelData data)
    {
        string json;
        string path = Application.persistentDataPath + levelSaveFileName;
        if (!File.Exists(path))
        {
            CreateLevel();
        }
        List<LevelData> levelDatas = LoadLevelDataList();
        levelDatas[index] = data;
        LevelSaveClass level = new LevelSaveClass(levelDatas);
        json = JsonUtility.ToJson(level);
        File.WriteAllText(path, json);
    }

    public static void SaveLevelDataList(List<LevelData> levelDatas)
    {
        string json;
        string path = Application.persistentDataPath + levelSaveFileName;
        if (!File.Exists(path))
        {
            CreateLevel();
        }
        LevelSaveClass level = new LevelSaveClass(levelDatas);
        json = JsonUtility.ToJson(level);
        File.WriteAllText(path, json);
    }

    public static void SaveLevelProgress(int index, float points, int stars)
    {
        string json;
        string path = Application.persistentDataPath + levelSaveFileName;
        if (!File.Exists(path))
        {
            CreateLevel();
        }
        List<LevelData> levelDatas = LoadLevelDataList();
        if (points > levelDatas[index].bestPoints) levelDatas[index].bestPoints = points;
        if (stars > levelDatas[index].stars) levelDatas[index].stars = stars;
        LevelSaveClass level = new LevelSaveClass(levelDatas);
        json = JsonUtility.ToJson(level);
        File.WriteAllText(path, json);
    }

    public static void AddLevelData(LevelData levelData)
    {
        string json;
        string path = Application.persistentDataPath + levelSaveFileName;
        if (!File.Exists(path))
        {
            CreateLevel();
        }
        List<LevelData> levelDatas = LoadLevelDataList();
        levelDatas.Add(levelData);
        LevelSaveClass level = new LevelSaveClass(levelDatas);
        json = JsonUtility.ToJson(level);
        File.WriteAllText(path, json);
    }

    public static void DeleteLevelData(int index)
    {
        string path = Application.persistentDataPath + levelSaveFileName;
        List<LevelData> levels = LoadLevelDataList();
        levels.RemoveAt(index);
        string json = JsonUtility.ToJson(new LevelSaveClass(levels));
        File.WriteAllText(path, json);
    }
}
