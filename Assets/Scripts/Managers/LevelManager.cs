using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private Ball ball;

    private int allBlocksCount;

    #endregion


    #region Events

    public static event Action OnAllBlocksDestroyed;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        KillZone.OnBallEntered += KillZoneOnBallEntered;
        Block.OnCreated += BlockOnCreated;
        Block.OnDestroyed += BlockOnDestroyed;
    }

    private void OnDisable()
    {
        KillZone.OnBallEntered -= KillZoneOnBallEntered;
        Block.OnCreated -= BlockOnCreated;
        Block.OnDestroyed -= BlockOnDestroyed;
    }

    #endregion


    #region Public methods

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNextScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var maxSceneIndex = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex < maxSceneIndex)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion


    #region Event Handlers

    private void KillZoneOnBallEntered()
    {
        ball.ResetBall();
    }

    private void BlockOnCreated()
    {
        allBlocksCount++;

        Debug.Log($"Блок создан. На сцене {allBlocksCount} блоков");
    }

    private void BlockOnDestroyed(int awardPoints)
    {
        allBlocksCount--;

        if (allBlocksCount <= 0)
        {
            allBlocksCount = 0;
            LoadNextScene();
            OnAllBlocksDestroyed?.Invoke();
        }

        Debug.Log($"Блок уничтожен. На сцене {allBlocksCount} блоков");
    }

    #endregion
}