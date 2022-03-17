using UnityEngine;
using System.IO;
public static class SaveSystemScript
{
    public static void SaveOption(Option option)
    {
        string path = Application.persistentDataPath + "/savefile.json";
        OptionData optionData = new OptionData(option);
        string json = JsonUtility.ToJson(optionData);
        File.WriteAllText(path, json);
    }

    public static OptionData LoadOption()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        string json = File.ReadAllText(path);
        OptionData optionData = JsonUtility.FromJson<OptionData>(json);
        return optionData;
    }
}
