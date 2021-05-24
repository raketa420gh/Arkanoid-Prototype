using UnityEngine;

public class PickUpAddScore : PickUpBase
{
    #region Variables

    [SerializeField] private int scoreToAdd;

    #endregion
    

    #region Protected methods

    protected override void ApplyEffect()
    {
        GameManager.Instance.AddTotalPoints(scoreToAdd);
    }

    #endregion
}
