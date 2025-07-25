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
        float timefall = 1.5f;
        rb.constraints = RigidbodyConstraints.None;
        Destroy(gameObject, timefall); 
    }
}
