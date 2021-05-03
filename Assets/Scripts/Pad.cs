using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Unity lifecycle

    private void Update()
    {
        Vector3 positionInPixels = Input.mousePosition;
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(positionInPixels);
        Vector3 padPosition = positionInWorld;

        padPosition.y = transform.position.y;
        padPosition.z = 0f;

        transform.position = padPosition;
    }

    #endregion
}