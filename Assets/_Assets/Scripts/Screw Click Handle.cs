using UnityEngine;

public class ScrewClickHandler : MonoBehaviour
{
    private Animator animator;
    private bool isRemoved = false;
    public Block parentBlock; 

    void Start()
    {
        animator = GetComponent<Animator>();

        if (parentBlock != null)
        {
            parentBlock.RegisterScrew();
        }
        GameManager.Instance.AddScrew(); 
    }

    public void OnClickManually()
    {
        if (isRemoved) return;

        animator.SetTrigger("Pull");
        Invoke(nameof(HideScrew), 0.5f);
        isRemoved = true;
    }

    void OnMouseDown()
    {
        if (isRemoved) return;

        animator.SetTrigger("Pull");
        Invoke(nameof(HideScrew), 0.5f);
        isRemoved = true;

        if (parentBlock != null)
        {
            parentBlock.OnScrewRemoved();
        }

        GameManager.Instance.RemoveScrew(); 
    }

    void HideScrew()
    {
        gameObject.SetActive(false);
        if (parentBlock != null)
        {
            parentBlock.OnScrewRemoved();
        }
    }
}
