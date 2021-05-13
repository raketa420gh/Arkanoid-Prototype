using System;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceView : MonoBehaviour
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
        UpdatePointsLabel();
        UpdateLivesLabel();
        HideGameOverPanel();
        HidePausePanel();
    }

    private void OnEnable()
    {
        Block.OnDestroyed += UpdatePointsLabel;
        KillZone.OnBallEnterKillZone += UpdateLivesLabel;
        GameManager.OnGameStarted += Start;
        GameManager.OnGameOvered += GameOverHandler;
        Pauser.OnGamePaused += ShowPausePanel;
        Pauser.OnGameUnpaused += HidePausePanel;
    }

    private void OnDisable()
    {
        Block.OnDestroyed -= UpdatePointsLabel;
        KillZone.OnBallEnterKillZone -= UpdateLivesLabel;
        GameManager.OnGameStarted -= Start;
        GameManager.OnGameOvered -= GameOverHandler;
        Pauser.OnGamePaused -= ShowPausePanel;
        Pauser.OnGameUnpaused -= HidePausePanel;
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

    #endregion


    #region Private methods
    
    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    private void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    private void UpdatePointsLabel()
    {
        pointsText.text = $"Очки: {GameManager.Instance.TotalPoints.ToString()}";
    }

    private void UpdateLivesLabel()
    {
        livesText.text = $"Жизни: {GameManager.Instance.CurrentLifes.ToString()}";
    }

    private void UpdateTotalPointsLabel()
    {
        totalPointsText.text = $"Всего заработано очков = {GameManager.Instance.TotalPoints.ToString()}";
    }

    #endregion


    #region Events Handlers

    private void GameOverHandler()
    {
        ShowGameOverPanel();
        UpdateTotalPointsLabel();
    }

    #endregion
}