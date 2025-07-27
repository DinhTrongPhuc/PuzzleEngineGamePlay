using UnityEngine;

public class Block : MonoBehaviour
{
    public int screwCount = 0;              
    private int removedScrews = 0;          
    private Rigidbody rb;
    private bool hasFallen = false;        

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        GameManager.Instance.RegisterBlock(this);
    }

    public void RegisterScrew()
    {
        screwCount++;
    }

    public void OnScrewRemoved()
    {
        removedScrews++;
        if (removedScrews >= screwCount && !hasFallen)
        {
            Fall();
        }
    }

    void Fall()
    {
        hasFallen = true;

        rb.constraints = RigidbodyConstraints.None;

        Invoke(nameof(Unregister), 5f);
    }

    void Unregister()
    {
        GameManager.Instance.UnregisterBlock(this);

        gameObject.SetActive(false);
    }

    public bool IsAllScrewsRemoved()
    {
        return removedScrews >= screwCount;
    }

}
