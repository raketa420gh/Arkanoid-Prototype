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
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject mainMenuPanel;
    

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        HideGameOverPanel();
        PausePanelVision(false);
    }

    private void OnEnable()
    {
        PauseManager.OnGamePausedOn += PauseManagerOnGamePausedOn;
    }

    private void OnDisable()
    {
        PauseManager.OnGamePausedOn -= PauseManagerOnGamePausedOn;
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

    public void PausePanelVision(bool isActive)
    {
        pausePanel.SetActive(isActive);
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


    #region Event Handlers

    private void PauseManagerOnGamePausedOn(bool isActive)
    {
        PausePanelVision(isActive);
    }

    #endregion
}