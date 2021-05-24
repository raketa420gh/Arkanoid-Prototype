using UnityEngine;

public abstract class PickUpBase: MonoBehaviour
{
    #region Unity lifecycle

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag(Tags.Pad))
        {
            ApplyEffect();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(Tags.KillZone))
        {
            Destroy(gameObject);
        }
    }

    #endregion


    #region Protected methods

    protected abstract void ApplyEffect();

    #endregion
}
