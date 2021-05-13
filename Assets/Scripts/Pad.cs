using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables

    [Header("Movement Limit")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    
    private Ball ball;
    private Pauser pauser;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        pauser = FindObjectOfType<Pauser>();
    }

    private void Update()
    {
        if (pauser.IsPaused)
        {
            return;
        }
            
        if (GameManager.Instance.IsAutoPlayOn)
        {
            Vector3 padPosition = ball.transform.position;
            padPosition.y = transform.position.y;
            padPosition.x = Mathf.Clamp(padPosition.x, minX, maxX);
            transform.position = padPosition;
        }
        else
        {
            Vector3 positionInPixels = Input.mousePosition;
            Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(positionInPixels);
            Vector3 padPosition = positionInWorld;

            padPosition.x = Mathf.Clamp(padPosition.x, minX, maxX);
            padPosition.y = transform.position.y;
            padPosition.z = 0f;
            transform.position = padPosition;
        }
    }

    #endregion
}