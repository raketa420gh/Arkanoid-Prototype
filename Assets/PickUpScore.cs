using UnityEngine;

public class PickUpScore : MonoBehaviour
{
    #region Unity lifecycle

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(Tags.Pad))
        {
            Debug.Log($"Вы подняли {gameObject}");
            Destroy(gameObject);
        }
    }

    #endregion
}
