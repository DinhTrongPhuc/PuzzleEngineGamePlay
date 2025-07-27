using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Level Holder")]
    public Transform levelHolder;
    private GameObject[] levelList;
    private int currentLevelIndex = 0;

    [Header("UI Management")]
    public GameUIManager uiManager;

    public List<Block> blocks = new List<Block>();
    public List<ScrewClickHandle> screwsOnBoard = new List<ScrewClickHandle>();
    public List<ScrewClickHandle> screwsInTemp = new List<ScrewClickHandle>();
    public List<ScrewClickHandle> screwsInBox = new List<ScrewClickHandle>();

    private int totalScrews = 0;
    private int removedScrews = 0;
    private List<Block> activeBlocks = new List<Block>();

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Instance = this;

        int childCount = levelHolder.childCount;
        levelList = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            levelList[i] = levelHolder.GetChild(i).gameObject;
            levelList[i].SetActive(false);
        }
        currentLevelIndex = 0;
        levelList[currentLevelIndex].SetActive(true);
        uiManager?.UpdateLevel(currentLevelIndex + 1);
        uiManager?.HideWinUI();
        uiManager?.HideLoseUI();
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
            Invoke(nameof(CheckWinCondition), 0.5f);
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

        if (removedScrews >= totalScrews && activeBlocks.Count == 0)
        {
            WinGame();
        }
    }

    public void RegisterScrew(ScrewClickHandle screw)
    {
        if (screw.state == ScrewState.OnBlock)
            screwsOnBoard.Add(screw);
        else if (screw.state == ScrewState.InTemp)
            screwsInTemp.Add(screw);
        else if (screw.state == ScrewState.InBox)
            screwsInBox.Add(screw);
    }

    public void UnregisterScrew(ScrewClickHandle screw)
    {
        screwsOnBoard.Remove(screw);
        screwsInTemp.Remove(screw);
        screwsInBox.Remove(screw);
    }

    void WinGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        Debug.Log("YOU WIN!");
        if (uiManager != null)
        {
            uiManager.ShowWinUI();
        }

        Invoke(nameof(NextLevel), 2f);
    }

    public void LoseGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        Debug.Log("YOU LOSE!");
        if (uiManager != null)
        {
            uiManager.ShowLoseUI();
        }
    }

    public void NextLevel()
    {
        if (currentLevelIndex + 1 >= levelList.Length)
        {
            Debug.Log("Đã hoàn thành tất cả level!");
            return;
        }

        levelList[currentLevelIndex].SetActive(false);
        currentLevelIndex++;

        levelList[currentLevelIndex].SetActive(true);

        blocks.Clear();
        screwsOnBoard.Clear();
        screwsInTemp.Clear();
        Block[] allBlocks = levelList[currentLevelIndex].GetComponentsInChildren<Block>();
        foreach (var block in allBlocks)
        {
            RegisterBlock(block);
        }

        uiManager?.HideWinUI();
        uiManager?.HideLoseUI();
        uiManager?.UpdateLevel(currentLevelIndex + 1);
    }

    public void ClearLevelData()
    {
        blocks.Clear();
        screwsOnBoard.Clear();
        screwsInTemp.Clear();
        screwsInBox.Clear();
    }

    public void RestartLevel()
    {

    }
}
