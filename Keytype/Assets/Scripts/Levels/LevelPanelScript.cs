using UnityEngine;

public class LevelPanelScript : MonoBehaviour
{
    public string levelName;
    [TextArea]
    public string levelDescription;

    public void CreateNewLevel()
    {
        SaveSystemScript.SaveLevel(new LevelData(levelName, levelDescription));
    }

    public void DeleteLevelByName()
    {
        SaveSystemScript.DeleteLevel(SaveSystemScript.LoadLevelByName(levelName));  
    }
}
