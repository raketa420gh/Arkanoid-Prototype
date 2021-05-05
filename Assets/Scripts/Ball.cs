using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private float speed;
    [SerializeField] private Transform padTransform;

    private bool isStarted;

    #endregion


    #region Unity lifecycle

    private void Update()
    {
        if (!isStarted)
        {
            Vector3 padPosition = padTransform.position;
            padPosition.y = transform.position.y;

            transform.position = padPosition;

            if (Input.GetMouseButtonDown(0))
            {
                LaunchBall();
            }
        }
    }

    #endregion


    #region Private methods

    private void LaunchBall()
    {
        Vector2 directionWithRandom = new Vector2(Random.Range(-0.5f, 0.5f), 1f).normalized;
        Vector2 forceDirection = directionWithRandom * speed;
        rigidBody2D.AddForce(forceDirection);
        isStarted = true;
    }

    #endregion
}