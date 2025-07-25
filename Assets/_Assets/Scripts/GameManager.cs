using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalScrews = 0;
    private int removedScrews = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public void AddScrew()
    {
        totalScrews++;
    }

    public void RemoveScrew()
    {
        removedScrews++;
        if (removedScrews >= totalScrews)
        {
            Invoke(nameof(NextLevel), 1f);
        }
    }

    void NextLevel()
    {
        Debug.Log("All screws removed! Loading next level...");
    }
}
