using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OptionPanelScript))]
public class OptionPanelEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        OptionPanelScript optionPanelScript = target as OptionPanelScript;
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Option"))
        {
            optionPanelScript.CreateOption();
        }
    }
}
