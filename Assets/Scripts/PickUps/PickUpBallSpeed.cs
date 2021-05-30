using UnityEngine;

public class PickUpBallSpeed : PickUpBase
{
    #region Variables

    [SerializeField] private float speedFactor;
    
    #endregion


    #region Protected methods

    protected override void ApplyEffect()
    {
        Debug.Log($"Скорость мяча изменена.");
    }

    #endregion
}