using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private Transform padTransform;
    
    [Header("Ball Settings")]
    [SerializeField] private float speed;
    
    private bool isLaunched;
    

    #region Properties

    public float Speed
    {
        get => speed;
        private set => speed = value;
    }

    #endregion
    

    #endregion
    

    #region Public methods

    public void ChangeSpeed(float speedFactor)
    {
        //rigidBody2D.velocity *= speedFactor;
    }

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
        KillZone.OnBallEntered += ResetBall;
    }

    private void OnDisable()
    {
        KillZone.OnBallEntered -= ResetBall;
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


    #region Public methods

    public void ResetBall()
    {
        transform.position = new Vector3(padTransform.position.x, padTransform.position.y + 0.75f, 0f);
        rigidBody2D.velocity = Vector2.zero;
        isLaunched = false;
    }

    #endregion


    #region Private methods
    
    private void LaunchBall()
    {
        Vector2 directionWithRandom = new Vector2(Random.Range(-0.5f, 0.5f), 1f).normalized;
        Vector2 forceDirection = directionWithRandom * Speed;
        rigidBody2D.AddForce(forceDirection);
        isLaunched = true;
    }

    #endregion
}