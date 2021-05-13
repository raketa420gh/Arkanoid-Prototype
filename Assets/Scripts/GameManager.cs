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
    
    public static event Action OnGameOvered;
    public static event Action OnLevelRestarted;

    public static event Action OnAllBlocksDestroyed;

    public static event Action OnLifesChanged;

    #endregion


    #region Properties

    public int CurrentLifes { get; set; }
    public int TotalPoints { get; set; }

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
        Block.OnCreated += AddAllBlocksAmount;
        Block.OnDestroyed += ReduceAllBlocksCount;
        KillZone.OnBallEnterKillZone += ReduceLive;
    }

    private void OnDisable()
    {
        Block.OnCreated -= AddAllBlocksAmount;
        Block.OnDestroyed -= ReduceAllBlocksCount;
        KillZone.OnBallEnterKillZone -= ReduceLive;
    }

    private void Update()
    {
        if (!isCheatsOn)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (CurrentLifes < maxLifes)
            {
                CurrentLifes++;
                OnLifesChanged?.Invoke();
            }
        }
    }

    #endregion


    #region Public methods

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Start();
    }

    public void StartMainMenuScene()
    {
        SceneManager.LoadScene(0);
        Start();
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        OnLevelRestarted?.Invoke();
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

    private void RestoreLifes()
    {
        CurrentLifes = maxLifes;
    }

    private void BuckOffPoints()
    {
        TotalPoints = 0;
    }


    private void AddAllBlocksAmount()
    {
        allBlocksCount++;
        Debug.Log($"Блок создан. Теперь на сцене {allBlocksCount} блоков");
    }

    private void ReduceAllBlocksCount()
    {
        allBlocksCount -= 1;
        Debug.Log($"Уничтожен блок. Осталось = {allBlocksCount}");

        if (allBlocksCount <= 0)
        {
            allBlocksCount = 0;
            OnAllBlocksDestroyed?.Invoke();
            LoadNextScene();
        }
    }

    #endregion
}