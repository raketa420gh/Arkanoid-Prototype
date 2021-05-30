public class PickUpStickyPad : PickUpBase
{
    #region Private Methods

    protected override void ApplyEffect()
    {
        var pad = FindObjectOfType<Pad>();

        if (pad != null)
        {
            pad.ApplyStickiness();
        }
    }
    
    #endregion
}
