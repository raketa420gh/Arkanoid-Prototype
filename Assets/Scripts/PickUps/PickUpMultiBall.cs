using UnityEngine;

public class PickUpMultiBall : PickUpBase
{
    #region Variables

    [SerializeField] private int additionalBallsAmount = 2;

    #endregion


    #region Private methods

    private void CloneBall(Ball ball)
    {
        for (int i = 0; i < additionalBallsAmount; i++)
        {
            var newBall = Instantiate(ball, ball.transform.position, ball.transform.rotation);
            newBall.LaunchBall();
        }
    }

    #endregion
    
    
    #region Protected methods

    protected override void ApplyEffect()
    {
        BallsTracker.Instance.PerformActionWithBalls(CloneBall);
    }

    #endregion
}
