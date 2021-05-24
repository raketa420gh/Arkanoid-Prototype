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
    [SerializeField] private GameObject topPanel;
    [SerializeField] private GameObject selectLevelPanel;
    
    #endregion


    #region Unity lifecycle

    private void Start()
    {
        GameOverPanelVision(false);
        PausePanelVision(false);
        SelectLevelPanelVision(false);
        TopPanelVision(true);
        MainMenuPanelVision(true);
    }

    private void OnEnable()
    {
        PauseManager.OnPausedOn += PauseManagerOnPausedOn;
    }

    private void OnDisable()
    {
        PauseManager.OnPausedOn -= PauseManagerOnPausedOn;
    }

    #endregion


    #region Public methods

    public void TopPanelVision(bool isActive)
    {
        topPanel.SetActive(isActive);
    }

    public void GameOverPanelVision(bool isActive)
    {
        gameOverPanel.SetActive(isActive);
    }

    public void PausePanelVision(bool isActive)
    {
        pausePanel.SetActive(isActive);
    }
    public void SelectLevelPanelVision(bool isActive)
    {
        selectLevelPanel.SetActive(isActive);
    }

    public void MainMenuPanelVision(bool isActive)
    {
        mainMenuPanel.SetActive(isActive);
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

    private void PauseManagerOnPausedOn(bool isActive)
    {
        PausePanelVision(isActive);
    }

    #endregion
}