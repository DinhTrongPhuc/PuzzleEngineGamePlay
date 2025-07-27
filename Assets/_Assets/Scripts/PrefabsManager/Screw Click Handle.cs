using UnityEngine;

public enum ScrewState
{
    OnBlock,
    InTemp,
    InBox
}
public class ScrewClickHandle : MonoBehaviour
{
    private Animator animator;
    private bool isRemoved = false;
    public Block parentBlock;

    public ScrewState state = ScrewState.OnBlock;

    void Start()
    {
        animator = GetComponent<Animator>();

        GameManager.Instance.RegisterScrew(this);

        if (parentBlock != null)
        {
            parentBlock.RegisterScrew(); 
        }

        GameManager.Instance.AddScrew(); 
    }

    void OnMouseDown()
    {
        if (isRemoved) return;

        isRemoved = true;
        animator.SetTrigger("Pull");

        Invoke(nameof(HideScrew), 0.5f);
    }

    void HideScrew()
    {
        gameObject.SetActive(false);

        if (parentBlock != null)
        {
            parentBlock.OnScrewRemoved();
        }

        GameManager.Instance.RemoveScrew();
    }

    public void OnClickManually()
    {
        if (isRemoved) return;

        isRemoved = true;
        animator.SetTrigger("Pull");
        Invoke(nameof(HideScrew), 0.5f);
    }

    public void MoveToTemp()
    {
        GameManager.Instance.UnregisterScrew(this);
        state = ScrewState.InTemp;
        GameManager.Instance.RegisterScrew(this);
    }

    public void MoveToBox()
    {
        GameManager.Instance.UnregisterScrew(this);
        state = ScrewState.InBox;
        GameManager.Instance.RegisterScrew(this);
    }

}
