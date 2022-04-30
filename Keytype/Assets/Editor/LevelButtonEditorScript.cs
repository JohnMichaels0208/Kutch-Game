using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelButtonScript))]
public class LevelsButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelButtonScript levelsButtonScript = (LevelButtonScript)target;

        base.OnInspectorGUI();

        if (GUILayout.Button("Save Level"))
        {
            levelsButtonScript.SaveLevel();
        }
        if (GUILayout.Button("Delete Level"))
        {
            levelsButtonScript.DeleteLevel();
        }
    }
}
