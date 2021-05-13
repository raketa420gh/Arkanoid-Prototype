using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables
    
    [Header("Game Settings")]
    [SerializeField] private int maxLifes;

    [Header("Other Managers")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private UIManager uiManager;
    
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
            CurrentLifePoints++;
            uiManager.UpdatePointsLabel(CurrentLifePoints);
        }
    }

    #endregion


    #region Private methods

    private void ReduceLive()
    {
        if (CurrentLifePoints > 1)
        {
            CurrentLifePoints--;
            uiManager.UpdateLifePointsLabel(CurrentLifePoints);
        }
        else
        {
            uiManager.ShowGameOverPanel();
            uiManager.UpdateTotalPointsLabel(TotalPoints);
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

    #endregion


    #region Event Handlers

    private void KillZoneOnBallEntered()
    {
        ReduceLive();
    }

    private void LevelManagerOnAllBlocksDestroyed()
    {
        levelManager.LoadNextScene();
    }

    private void BlockOnDestroyed(int awardPoints)
    {
        TotalPoints += awardPoints;
        uiManager.UpdatePointsLabel(TotalPoints);
    }

    #endregion
}