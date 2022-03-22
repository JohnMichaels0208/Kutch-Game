using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonLoadLevelScript : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    public void LoadLevel()
    {
        if (targetSceneName != null)
        {
            Debug.Log(CheckSceneExists(targetSceneName));
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
            nameOfIteratedScene = path.Substring(path.LastIndexOf(".unity") - path.Length - 5);
            Debug.Log(nameOfIteratedScene);
            if(SceneManager.GetSceneByBuildIndex(i).name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
