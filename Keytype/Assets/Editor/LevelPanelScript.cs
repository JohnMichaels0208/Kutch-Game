using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelPanelScript))]
public class LevelsPanelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelPanelScript levelsPanelScript = (LevelPanelScript)target;

        base.OnInspectorGUI();

        if (GUILayout.Button("Create New Level"))
        {
            levelsPanelScript.CreateNewLevel();
        }
        if (GUILayout.Button("Delete Level"))
        {
            levelsPanelScript.DeleteLevelByName();
        }
    }
}
