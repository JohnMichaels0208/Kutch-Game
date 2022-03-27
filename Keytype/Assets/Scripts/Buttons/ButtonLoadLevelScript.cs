using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonLoadLevelScript : MonoBehaviour
{
    public string targetSceneName;
    public void LoadLevel()
    {
        if (targetSceneName != null)
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
