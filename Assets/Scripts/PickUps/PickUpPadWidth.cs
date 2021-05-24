using UnityEngine;

public class PickUpPadWidth : PickUpBase
{
    #region Variables

    [SerializeField] private float widthFactor;
    
    private Pad pad;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        pad = FindObjectOfType<Pad>();
    }

    #endregion
    
    
    #region Protected methods

    protected override void ApplyEffect()
    {
        pad.ChangePadWigth(widthFactor);
        Debug.Log($"Ширина пада изменилась.");
    }

    #endregion
}
