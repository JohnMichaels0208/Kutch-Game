using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class SaveSystemScript
{
    public const string optionSaveFileName = "/optionsavefile.json";

    public const string progressSaveFileName = "/progresssavefile.json";

    public const string levelSaveFileName = "/levelsavefile.json";

    public static void SaveOption(Option option)
    {
        string path = Application.persistentDataPath + optionSaveFileName;
        OptionData optionData = new OptionData(option.soundEffectsVolume, option.gameSoundVolume, option.isFullScreen);
        string json = JsonUtility.ToJson(optionData);
        File.WriteAllText(path, json);
    }

    public static OptionData LoadOption()
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

    public static List<LevelData> LoadLevelList()
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

    public static int LoadLevelIndexByGO(GameObject button)
    {
        List<LevelData> levelDatas = LoadLevelList();
        for (int i = 0; i < levelDatas.Count; i++)
        {
            if (ReferenceEquals(levelDatas[i].associatedButton, button))
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
        List<LevelData> levelDatas = LoadLevelList();
        levelDatas[index] = data;
        LevelSaveClass level = new LevelSaveClass(levelDatas);
        json = JsonUtility.ToJson(level);
        File.WriteAllText(path, json);
    }

    public static void SaveLevelProgress(int index, float bestPoints, int stars)
    {
        string json;
        string path = Application.persistentDataPath + levelSaveFileName;
        if (!File.Exists(path))
        {
            CreateLevel();
        }
        List<LevelData> levelDatas = LoadLevelList();
        levelDatas[index].bestPoints = bestPoints;
        levelDatas[index].stars = stars;
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
        List<LevelData> levelDatas = LoadLevelList();
        levelDatas.Add(levelData);
        LevelSaveClass level = new LevelSaveClass(levelDatas);
        json = JsonUtility.ToJson(level);
        File.WriteAllText(path, json);
    }

    public static void DeleteLevelData(int index)
    {
        string path = Application.persistentDataPath + levelSaveFileName;
        List<LevelData> levels = LoadLevelList();
        levels.RemoveAt(index);
        string json = JsonUtility.ToJson(new LevelSaveClass(levels));
        File.WriteAllText(path, json);
    }
}
