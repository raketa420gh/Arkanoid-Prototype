using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables

    [Header("Texts")] 
    [SerializeField] private Text pointsText;
    [SerializeField] private Text livesText;
    [SerializeField] private Text totalPointsText;

    [Header("Panels")] 
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject mainMenuPanel;
    

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        HideGameOverPanel();
        HidePausePanel();
    }

    private void OnEnable()
    {
        PauseManager.OnGamePaused += ShowPausePanel;
        PauseManager.OnGameUnpaused += HidePausePanel;
    }

    private void OnDisable()
    {
        PauseManager.OnGamePaused -= ShowPausePanel;
        PauseManager.OnGameUnpaused -= HidePausePanel;
    }

    #endregion


    #region Public methods

    public void ShowMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
    }

    public void HideMainMenuPanel()
    {
        mainMenuPanel.SetActive(false);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
    
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }
    
    public void UpdatePointsLabel(int points)
    {
        pointsText.text = $"Очки: {points}";
    }

    public void UpdateLifePointsLabel(int currentLifePoints)
    {
        livesText.text = $"Жизни: {currentLifePoints}";
    }

    public void UpdateTotalPointsLabel(int totalPoints)
    {
        totalPointsText.text = $"Всего заработано очков = {totalPoints}";
    }

    #endregion
}