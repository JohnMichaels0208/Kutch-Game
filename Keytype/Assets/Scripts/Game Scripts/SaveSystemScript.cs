using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystemScript
{
    public static void SaveOption(Option option)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/option.data";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        OptionData optionData = new OptionData(option);

        formatter.Serialize(fileStream, optionData);
        fileStream.Close();
    }

    public static OptionData LoadOption()
    {
        string path = Application.persistentDataPath + "/option.data";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            binaryFormatter.Deserialize(fileStream);
            OptionData optionData = binaryFormatter.Deserialize(fileStream) as OptionData;
            fileStream.Close();
            
            return optionData;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
