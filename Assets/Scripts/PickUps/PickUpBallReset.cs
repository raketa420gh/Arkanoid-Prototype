using UnityEngine;

public class PickUpBallReset : PickUpBase
{
    #region Variables

    private Ball ball;

    #endregion
    
    
    #region Unity lifecycle

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
    }

    #endregion
    
    
    #region Protected methods
    
    protected override void ApplyEffect()
    {
        ball.ResetBall();
        Debug.Log($"Положение мяча сброшено.");
    }

    #endregion
}
