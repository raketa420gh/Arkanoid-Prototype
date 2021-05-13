using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables
    
    [Header("Game Settings")]
    [SerializeField] private int maxLifes;
    
    [Header("Dev Only")]
    [SerializeField] private bool isAutoPlayOn;
    [SerializeField] private bool isCheatsOn;
    
    private int allBlocksCount;

    #endregion


    #region Events

    public static event Action OnGameStarted;
    public static event Action OnGameOvered;

    #endregion


    #region Properties

    public int CurrentLifes { get; set; }
    public int TotalPoints { get; set; }

    public bool IsAutoPlayOn => isAutoPlayOn;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        RestoreLives();
        BuckOffPoints();
        OnGameStarted?.Invoke();
    }

    private void OnEnable()
    {
        Block.OnCreated += CalculateAllBlocks;
        Block.OnDestroyed += ReduceBlocksCount;
        KillZone.OnBallEnterKillZone += ReduceLive;
    }

    private void OnDisable()
    {
        Block.OnCreated -= CalculateAllBlocks;
        Block.OnDestroyed -= ReduceBlocksCount;
        KillZone.OnBallEnterKillZone -= ReduceLive;
    }

    private void Update()
    {
        if (!isCheatsOn)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.F))
        {
            return;
        }
        
        if (CurrentLifes < maxLifes)
        {
            CurrentLifes++;
        }
    }

    #endregion


    #region Public methods

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Start();
    }

    public void StartMainMenuLevel()
    {
        SceneManager.LoadScene(0);
        OnGameStarted?.Invoke();
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion


    #region Private methods

    private void ReduceLive()
    {
        if (CurrentLifes > 0)
        {
            CurrentLifes--;
        }
        else
        {
            OnGameOvered?.Invoke();
        }
    }

    private void RestoreLives()
    {
        CurrentLifes = maxLifes;
    }

    private void BuckOffPoints()
    {
        TotalPoints = 0;
    }


    private void CalculateAllBlocks()
    {
        allBlocksCount++;
        Debug.Log($"Блок создан. Теперь на сцене {allBlocksCount} блоков");
    }

    private void ReduceBlocksCount()
    {
        allBlocksCount -= 1;
        Debug.Log($"Уничтожен блок. Осталось = {allBlocksCount}");

        if (allBlocksCount <= 0)
        {
            allBlocksCount = 0;
            LoadNextScene();
        }
    }

    #endregion
}