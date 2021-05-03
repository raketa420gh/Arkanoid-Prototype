using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private float speed;
    [SerializeField] private Transform padTransform;

    private Vector2 directionWithOffset;
    private bool isStarted;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        directionWithOffset = new Vector2(Random.Range(-0.5f, 0.5f), 1f);
    }

    private void Update()
    {
        if (!isStarted)
        {
            Vector3 padPosition = padTransform.position;
            padPosition.y = transform.position.y;

            transform.position = padPosition;

            if (Input.GetMouseButtonDown(0))
            {
                PushBall();
            }
        }
    }

    #endregion
    

    #region Private methods

    private void PushBall()
    {
        Vector2 forceDirection = directionWithOffset * speed;
        rigidBody2D.AddForce(forceDirection);
        isStarted = true;
    }

    #endregion
}