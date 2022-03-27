using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
public static class SaveSystemScript
{
    public const string optionSaveFileName = "/optionsavefile.json";

    public const string progressSaveFileName = "/progresssavefile.json";

    public const string levelSaveFileName = "/levelsavefile.json";

    public static void SaveOption(Option option)
    {
        string path = Application.persistentDataPath + optionSaveFileName;
        OptionData optionData = new OptionData(option.soundEffectsVolume, option.isFullScreen);
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
        OptionData optionData = new OptionData(0, true);
        string json = JsonUtility.ToJson(optionData);
        File.WriteAllText(path, json);
    }

    public static void SaveProgress()
    {
        string path = Application.persistentDataPath + progressSaveFileName;
        ProgressData progressData = new ProgressData(GameManagerScript.instance.pointsToSave);
        string json = JsonUtility.ToJson(progressData);
        File.WriteAllText(path, json);
    }

    public static ProgressData LoadProgress()
    {
        string path = Application.persistentDataPath + progressSaveFileName;
        Debug.Log(path);
        if (!File.Exists(path))
        {
            CreateProgress();
        }
        string json = File.ReadAllText(path);
        ProgressData progressData = JsonUtility.FromJson<ProgressData>(json);
        return progressData;
    }

    public static void CreateProgress()
    {
        string path = Application.persistentDataPath + progressSaveFileName;
        ProgressData progressData = new ProgressData(0);
        string json = JsonUtility.ToJson(progressData);
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

    public static LevelData LoadLevelByName(string name)
    {
        List<LevelData> levels = LoadLevelList();
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].levelName == name)
            {
                return levels[i];
            }
        }
        return null;
    }

    public static void SaveLevel(LevelData levelData)
    {
        string json;
        string path = Application.persistentDataPath + levelSaveFileName;
        bool levelexists = false;
        if (!File.Exists(path))
        {
            CreateLevel();
        }
        List<LevelData> levelsData = LoadLevelList();
        if (levelsData.Count == 0)
        {
            levelsData = new List<LevelData> { levelData };
        }
        if (levelsData.Count > 0)
        {
            for (int i = 0; i < levelsData.Count; i++)
            {
                if (levelsData[i].levelName.Equals(levelData.levelName))
                {
                    levelsData[i] = levelData;
                    levelexists = true;
                }
            }
            if (!levelexists)
            {
                levelsData.Add(levelData);
            }

        }
        LevelSaveClass level = new LevelSaveClass(levelsData);
        json = JsonUtility.ToJson(level);
        Debug.Log(json);
        File.WriteAllText(path, json);
    }

    public static void DeleteLevel(LevelData levelData)
    {
        string path = Application.persistentDataPath + levelSaveFileName;
        List<LevelData> levels = LoadLevelList();
        for (int i = 0; i<levels.Count; i++)
        {
            if (levelData.levelName == levels[i].levelName)
            {
                levels.RemoveAt(i);
            }
        }
        string json = JsonUtility.ToJson(new LevelSaveClass(levels));
        Debug.Log(json);
        File.WriteAllText(path, json);
    }
}
