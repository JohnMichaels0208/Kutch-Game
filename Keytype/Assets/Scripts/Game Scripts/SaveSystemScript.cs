using UnityEngine;
using System.IO;
public static class SaveSystemScript
{
    public const string optionSaveFileName = "/optionsavefile.json";

    public const string progressSaveFileName = "/progresssavefile.json";

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
}
