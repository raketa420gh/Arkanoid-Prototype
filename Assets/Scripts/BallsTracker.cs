using System;
using System.Collections.Generic;

public class BallsTracker : SingletonMonoBehaviour<BallsTracker>
{
    #region Variables

    private List<Ball> balls = new List<Ball>();

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        Ball.OnCreated += BallOnCreated;
        Ball.OnDestroyed += BallOnDestroyed;
    }

    private void OnDisable()
    {
        Ball.OnCreated -= BallOnCreated;
        Ball.OnDestroyed -= BallOnDestroyed;
    }

    #endregion


    #region Public Methods

    public void PerformActionWithBalls(Action<Ball> action)
    {
        foreach (var ball in balls)
        {
            action?.Invoke(ball);
        }
    }

    #endregion


    #region Event Handlers

    private void BallOnCreated(Ball ball)
    {
        balls.Remove(ball);
    }

    private void BallOnDestroyed(Ball ball)
    {
        balls.Add(ball);
    }

    #endregion
}
