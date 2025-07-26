using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Level Management")]
    public Transform levelHolder;              
    private int currentLevelIndex = 0;

    [Header("UI Management")]
    public GameUIManager uiManager;            

    private int totalScrews = 0;
    private int removedScrews = 0;
    private List<Block> activeBlocks = new List<Block>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        foreach (Transform level in levelHolder)
        {
            level.gameObject.SetActive(false);
        }

        if (levelHolder.childCount >= 0)
        {
            levelHolder.GetChild(currentLevelIndex).gameObject.SetActive(true);
        }

        if (uiManager != null)
        {
            uiManager.HideWinUI();
        }
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
            Invoke(nameof(CheckWinCondition), 1f);
        }
    }

    void CheckWinCondition()
    {
        if (activeBlocks.Count == 0)
        {
            WinGame();
        }
    }

    public void RegisterBlock(Block block)
    {
        if (!activeBlocks.Contains(block))
            activeBlocks.Add(block);
    }

    public void UnregisterBlock(Block block)
    {
        if (activeBlocks.Contains(block))
            activeBlocks.Remove(block);

        if (activeBlocks.Count == 0) //&& removedScrews >= totalScrews)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");
        if (uiManager != null)
        {
            uiManager.ShowWinUI();
        }

        Invoke(nameof(NextLevel), 2f); 
    }

    void NextLevel()
    {
        if (currentLevelIndex < levelHolder.childCount)
        {
            levelHolder.GetChild(currentLevelIndex).gameObject.SetActive(false);
        }

        currentLevelIndex++;

        if (currentLevelIndex < levelHolder.childCount)
        {
            
            levelHolder.GetChild(currentLevelIndex).gameObject.SetActive(true);

            
            totalScrews = 0;
            removedScrews = 0;
            activeBlocks.Clear();

            if (uiManager != null)
                uiManager.HideWinUI();
        }
        else
        {
            Debug.Log("Đã hoàn thành tất cả các màn chơi!");
            // TODO: Hiện UI End Game hoặc Restart
        }
    }
}
