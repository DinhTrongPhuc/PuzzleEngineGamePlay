using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public Button buttonZoomIn;
    public Button buttonZoomOut;
    public Button buttonRetry;
    public TextMeshProUGUI textLevel;
    public GameObject losePanel; 

    public Transform targetToZoom; 
    public float zoomStep = 0.1f;
    public int currentLevel = 1;

    private void Start()
    {
        buttonZoomIn.onClick.AddListener(() => Zoom(1));
        buttonZoomOut.onClick.AddListener(() => Zoom(-1));
        buttonRetry.onClick.AddListener(RetryLevel);

        UpdateLevelText();
        HideLoseUI();
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
        losePanel.SetActive(true);
    }

    public void HideLoseUI()
    {
        losePanel.SetActive(false);
    }

    void UpdateLevelText()
    {
        textLevel.text = "Level: " + currentLevel;
    }
}
