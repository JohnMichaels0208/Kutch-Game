using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class ButtonLoadLevelScript : MonoBehaviour
{
    public string targetSceneName;
    public virtual string sceneToLoad { get { return targetSceneName; } }
    
    public virtual void LoadLevel()
    {
        List<LevelData> levelDatas = SaveSystemScript.LoadLevelDataList();
        if (sceneToLoad.Length > 0)
        {
            if (CheckSceneExists(sceneToLoad))
            {
                Debug.Log("loading scene");
                SceneManager.LoadScene(sceneToLoad);
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
