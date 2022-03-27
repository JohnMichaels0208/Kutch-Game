using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonScript : MonoBehaviour
{
    private Animator animator;
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
}
