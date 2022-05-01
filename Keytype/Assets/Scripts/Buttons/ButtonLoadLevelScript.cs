using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class ButtonLoadLevelScript : MonoBehaviour
{
    public string targetSceneName;
    
    public void LoadLevel()
    {
        List<LevelData> levelDatas = SaveSystemScript.LoadLevelDataList();
        for (int i = 0; i < levelDatas.Count; i++)
        {
            Debug.Log(levelDatas[i].associatedButton);
        }
        if (targetSceneName.Length > 0)
        {
            if (CheckSceneExists(targetSceneName))
            {
                SceneManager.LoadScene(targetSceneName);
            }
        }
    }

    private bool CheckSceneExists(string sceneName)
    {
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string nameOfIteratedScene = path.Substring(path.LastIndexOf('/') + 1);
            nameOfIteratedScene = nameOfIteratedScene.Substring(0, nameOfIteratedScene.LastIndexOf(".unity"));
            if(SceneManager.GetSceneByBuildIndex(i).name == nameOfIteratedScene)
            {
                return true;
            }
        }
        return false;
    }
}
