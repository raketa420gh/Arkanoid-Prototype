
public class PickUpAddLife : PickUpBase
{
    #region Protected methods
    
    protected override void ApplyEffect()
    {
        GameManager.Instance.AddCurrentLife();
    }
    
    #endregion
}
