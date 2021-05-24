using UnityEngine;

public class PickUpReduceScore : PickUpBase
{
    #region Variables

    [SerializeField] private int scoreToReduce;

    #endregion
    

    #region Protected methods

    protected override void ApplyEffect()
    {
        GameManager.Instance.ReduceTotalPoints(scoreToReduce);
    }

    #endregion
}
