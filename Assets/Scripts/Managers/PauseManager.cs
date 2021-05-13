using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    #region Properties

    public bool IsPaused { get; private set; }

    #endregion


    #region Events

    public static event Action OnGamePaused;
    public static event Action OnGameUnpaused;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        TogglePause(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                TogglePause(false);
            }
            else
            {
                TogglePause(true);
            }
        }
    }

    #endregion


    #region Public methods

    public void TogglePause(bool isActive)
    {
        Time.timeScale = isActive ? 0f : 1f;

        switch (isActive)
        {
            case true:
                IsPaused = true;
                OnGamePaused?.Invoke();
                break;
            case false:
                IsPaused = false;
                OnGameUnpaused?.Invoke();
                break;
        }
    }

    #endregion


    #region Private methods

    private void PauseOn()
    {
        TogglePause(true);
    }

    #endregion
}