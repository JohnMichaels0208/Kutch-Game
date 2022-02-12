using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string targetSceneName;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnHover()
    {
        animator.SetBool("IsHover", true);
    }

    public void OnHoverExit()
    {
        animator.SetBool("IsHover", false);
    }

    public void OnSelect()
    {
        if (targetSceneName != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
        }
    }
}
