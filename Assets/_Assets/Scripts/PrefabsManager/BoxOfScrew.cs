using System.Collections.Generic;
using UnityEngine;

public class BoxOfScrew : MonoBehaviour
{
    public Color color;
    public int maxScrews = 3;

    private List<Screw> screws = new List<Screw>();

    public void AddScrew(Screw screw)
    {
        screws.Add(screw);
        screw.MoveToBox(transform.position);

        if (screws.Count >= maxScrews)
        {
            Destroy(gameObject);
            GameUIManager.Instance.CreateNewBox();
        }
    }

    public bool IsFull()
    {
        return screws.Count >= maxScrews;
    }
}
