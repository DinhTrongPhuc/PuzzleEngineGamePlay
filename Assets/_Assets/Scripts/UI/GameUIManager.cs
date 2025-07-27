using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button buttonZoomIn;
    public Button buttonZoomOut;
    public Button buttonRetry;

    [Header("UI Panels")]
    public GameObject losePanel;
    public GameObject winPanel;

    [Header("Text UI")]
    public TextMeshProUGUI textLevel;

    [Header("Zoom Target")]
    public Transform targetToZoom;
    public float zoomStep = 0.1f;

    [Header("Level Info")]
    public int currentLevel = 1;

    [Header("Temp Slot Setup")]
    public List<Transform> tempSlots; 

    public static GameUIManager Instance;
    public int maxTempSlot = 3;

    private List<Block> blocks = new List<Block>();
    private List<Screw> screwsOnBoard = new List<Screw>();
    private List<Screw> screwsInTemp = new List<Screw>();
    private List<BoxOfScrew> boxes = new List<BoxOfScrew>();

    public GameObject boxPrefab;
    public Transform boxContainer;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (buttonZoomIn != null) buttonZoomIn.onClick.AddListener(() => Zoom(1));
        if (buttonZoomOut != null) buttonZoomOut.onClick.AddListener(() => Zoom(-1));
        if (buttonRetry != null) buttonRetry.onClick.AddListener(RetryLevel);

        HideWinUI();
        HideLoseUI();
        UpdateLevelText();
    }

    public Vector3 GetTempSlotPosition(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < tempSlots.Count)
        {
            return tempSlots[slotIndex].position;
        }
        else
        {
            Debug.LogWarning("Invalid temp slot index!");
            return Vector3.zero;
        }
    }

    public void RegisterBlock(Block block)
    {
        blocks.Add(block);
    }

    public void CreateNewBox()
    {
        Color newColor = GetRandomColor();
        GameObject newBoxObj = Instantiate(boxPrefab, boxContainer);
        BoxOfScrew newBox = newBoxObj.GetComponent<BoxOfScrew>();
        newBox.color = newColor;
        RegisterBox(newBox);
    }

    private Color GetRandomColor()
    {
        Color[] colorList = new Color[]
        {
        Color.red,
        Color.green,
        Color.blue,
        //Color.yellow,
        //Color.cyan,
        };
        int index = Random.Range(0, colorList.Length);
        return colorList[index];
    }


    public void RegisterBox(BoxOfScrew box)
    {
        boxes.Add(box);
    }

    public void OnScrewRemoved(Screw screw)
    {
        screwsOnBoard.Remove(screw);

        BoxOfScrew matchingBox = boxes.Find(box => box.color == screw.color && !box.IsFull());

        if (matchingBox != null)
        {
            matchingBox.AddScrew(screw);
        }
        else
        {
            if (screwsInTemp.Count < maxTempSlot)
            {
                screwsInTemp.Add(screw);
                screw.MoveToTempSlot(screwsInTemp.Count - 1);
            }
            else
            {
                GameOver();
            }
        }

        CheckWinCondition();
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER - Không còn chỗ chứa screw!");
        ShowLoseUI();
    }

    private void CheckWinCondition()
    {
        if (blocks.TrueForAll(block => block.IsAllScrewsRemoved()))
        {
            Debug.Log("YOU WIN!");
            ShowWinUI();
        }
    }

    void Zoom(int direction)
    {
        if (targetToZoom != null)
        {
            float scale = direction > 0 ? 1 + zoomStep : 1 - zoomStep;
            targetToZoom.localScale *= scale;
        }
    }

    void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowLoseUI()
    {
        if (losePanel != null)
            losePanel.SetActive(true);
    }

    public void HideLoseUI()
    {
        if (losePanel != null)
            losePanel.SetActive(false);
    }

    public void ShowWinUI()
    {
        if (winPanel != null)
            winPanel.SetActive(true);
    }

    public void HideWinUI()
    {
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    public void UpdateLevel(int level)
    {
        currentLevel = level;
        UpdateLevelText();
    }

    public void UpdateLevelText()
    {
        if (textLevel != null)
            textLevel.text = "Level: " + currentLevel;
    }
}
