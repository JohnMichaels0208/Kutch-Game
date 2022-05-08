using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButtonScript : ButtonLoadLevelScript
{
    public override string sceneToLoad { get { return SceneManager.GetActiveScene().name; } }
}
