using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private Transform padTransform;
    [SerializeField] private float speed;

    private bool isLaunched;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        ResetBall();
        
        if (GameManager.Instance.IsAutoPlayOn)
        {
            LaunchBall();
        }
    }

    private void OnEnable()
    {
        KillZone.OnBallEnterKillZone += ResetBall;
    }

    private void OnDisable()
    {
        KillZone.OnBallEnterKillZone -= ResetBall;
    }

    private void Update()
    {
        if (!isLaunched)
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

    private void ResetBall()
    {
        transform.position = new Vector3(padTransform.position.x, padTransform.position.y + 0.75f, 0f);
        rigidBody2D.velocity = Vector2.zero;
        isLaunched = false;
    }

    private void LaunchBall()
    {
        Vector2 directionWithRandom = new Vector2(Random.Range(-0.5f, 0.5f), 1f).normalized;
        Vector2 forceDirection = directionWithRandom * speed;
        rigidBody2D.AddForce(forceDirection);
        isLaunched = true;
    }

    #endregion
}