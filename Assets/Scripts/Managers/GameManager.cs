using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables

    [Header("Game Settings")] 
    [SerializeField] private int maxLifes;

    [Header("Other Managers")] 
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private UIManager uiManager;

    [Header("Game Objects")]
    [SerializeField] private Ball ball;

    [Header("Dev Only")] 
    [SerializeField] private bool isAutoPlayOn;
    [SerializeField] private bool isCheatsOn;

    #endregion


    #region Properties

    public int CurrentLifePoints { get; private set; }
    public int TotalPoints { get; private set; }

    public bool IsAutoPlayOn => isAutoPlayOn;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        RestoreLifes();
        BuckOffPoints();
        UpdateAllUI();
    }

    private void OnEnable()
    {
        Block.OnDestroyed += BlockOnDestroyed;
        KillZone.OnBallEntered += KillZoneOnBallEntered;
        LevelManager.OnAllBlocksDestroyed += LevelManagerOnAllBlocksDestroyed;
    }

    private void OnDisable()
    {
        Block.OnDestroyed -= BlockOnDestroyed;
        KillZone.OnBallEntered -= KillZoneOnBallEntered;
        LevelManager.OnAllBlocksDestroyed -= LevelManagerOnAllBlocksDestroyed;
    }

    private void Update()
    {
        if (!isCheatsOn)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && (CurrentLifePoints < maxLifes))
        {
            AddCurrentLife();
        }
    }

    #endregion


    #region Public methods

    public void AddTotalPoints(int addValue)
    {
        TotalPoints += addValue;
        uiManager.UpdateTotalPointsLabel(TotalPoints);
    }

    public void ReduceTotalPoints(int reduceValue)
    {
        TotalPoints -= reduceValue;

        if (TotalPoints <= 0)
        {
            TotalPoints = 0;
        }

        uiManager.UpdateTotalPointsLabel(TotalPoints);
        Debug.Log($"Вы потеряли {reduceValue} очков");
    }

    public void RestartLevel()
    {
        levelManager.OpenScene(1);
        FindObjectOfType<Ball>().ResetBall();
        CloseAllPanels();
        uiManager.TopPanelVision(true);
        RestoreLifes();
        BuckOffPoints();
        UpdateAllUI();
    }

    public void OpenMainMenu()
    {
        levelManager.OpenScene(0);
        CloseAllPanels();
        uiManager.MainMenuPanelVision(true);
        RestoreLifes();
        BuckOffPoints();
        UpdateAllUI();
    }

    public void AddCurrentLife()
    {
        if (CurrentLifePoints < maxLifes)
        {
            CurrentLifePoints++;
            uiManager.UpdatePointsLabel(CurrentLifePoints);
        }
        else
        {
            CurrentLifePoints = maxLifes;
            uiManager.UpdatePointsLabel(CurrentLifePoints);
        }
    }

    public void CloseAllPanels()
    {
        uiManager.GameOverPanelVision(false);
        uiManager.PausePanelVision(false);
        uiManager.TopPanelVision(false);
        uiManager.MainMenuPanelVision(false);
        uiManager.SelectLevelPanelVision(false);
        uiManager.WinPanelVision(false);
    }

    #endregion


    #region Private methods

    private void ReduceLive()
    {
        if (CurrentLifePoints > 1)
        {
            CurrentLifePoints--;
            uiManager.UpdateLifePointsLabel(CurrentLifePoints);
            Debug.Log($"Вы потеряли жизнь. Осталось {CurrentLifePoints}");
        }
        else
        {
            GameOver();
        }
    }

    private void RestoreLifes()
    {
        CurrentLifePoints = maxLifes;
        uiManager.UpdateLifePointsLabel(CurrentLifePoints);
    }

    private void BuckOffPoints()
    {
        TotalPoints = 0;
        uiManager.UpdateTotalPointsLabel(TotalPoints);
    }

    private void UpdateAllUI()
    {
        uiManager.UpdatePointsLabel(TotalPoints);
        uiManager.UpdateLifePointsLabel(CurrentLifePoints);
    }

    private void GameOver()
    {
        pauseManager.TogglePause(true);
        CloseAllPanels();
        uiManager.UpdateTotalPointsLabel(TotalPoints);
        uiManager.GameOverPanelVision(true);
    }

    #endregion


    #region Event Handlers

    private void KillZoneOnBallEntered()
    {
        ReduceLive();
    }

    private void LevelManagerOnAllBlocksDestroyed()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            CloseAllPanels();
            uiManager.WinPanelVision(true);
        }
        
        levelManager.LoadNextScene();
    }

    private void BlockOnDestroyed(int awardPoints)
    {
        TotalPoints += awardPoints;
        uiManager.UpdatePointsLabel(TotalPoints);
    }

    #endregion
}