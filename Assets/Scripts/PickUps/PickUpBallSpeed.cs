using UnityEngine;

public class PickUpBallSpeed : PickUpBase
{
    #region Variables

    [SerializeField] private float speedFactor;
        
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
        ball.ChangeSpeed(speedFactor);
        Debug.Log($"Скорость мяча изменена. Теперь она {ball.Speed}");
    }

    #endregion
}