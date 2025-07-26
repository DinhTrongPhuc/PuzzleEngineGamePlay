using UnityEngine;

public class Block : MonoBehaviour
{
    public int screwCount = 0;
    private int removedScrews = 0;
    private Rigidbody rb;

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
        if (removedScrews >= screwCount)
        {
            Fall();
        }
    }

    void Fall()
    {
        float TimeDelay = 2f;
        rb.constraints = RigidbodyConstraints.None;
        Invoke(nameof(Unregister), TimeDelay);
    }

    void Unregister()
    {
        GameManager.Instance.UnregisterBlock(this);
        gameObject.SetActive(false); 
    }
}
