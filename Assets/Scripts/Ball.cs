using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [Header("Components")] 
    [SerializeField] private Rigidbody2D rigidBody2D;

    [Header("Ball Settings")] 
    [SerializeField] private float speed;
    
    private Transform padTransform;
    private bool isLaunched;
    private Vector2 padOffset;
    
    #endregion


    #region Events

    public static event Action<Ball> OnCreated;
    public static event Action<Ball> OnDestroyed;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        padTransform = FindObjectOfType<Pad>().transform;
        
        CenterWithPad();
        CalculatePadOffset();
        OnCreated?.Invoke(this);
    }

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
            MoveWithPad();
            
            if (Input.GetMouseButtonDown(0))
            {
                LaunchBall();
            }
        }
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    #endregion


    #region Public methods

    public void LaunchBall()
    {
        Vector2 directionWithRandom = new Vector2(Random.Range(-0.5f, 0.5f), 1f).normalized;
        Vector2 forceDirection = directionWithRandom * speed;
        rigidBody2D.AddForce(forceDirection);
        isLaunched = true;
    }

    public void ResetBall()
    {
        transform.position = new Vector3(padTransform.position.x, padTransform.position.y + 0.75f, 0f);
        rigidBody2D.velocity = Vector2.zero;
        isLaunched = false;
    }
    
    public void Stick()
    {
        isLaunched = false;
        rigidBody2D.velocity = Vector2.zero;
        
        CalculatePadOffset();
    }

    #endregion


    #region Private methods

    private void MoveWithPad()
    {
        Vector2 padPosition = padTransform.position;
        padPosition -= padOffset;
        
        transform.position = padPosition;
    }

    private void CalculatePadOffset()
    {
        padOffset = padTransform.position - transform.position;
    }

    private void CenterWithPad()
    {
        var padPosition = padTransform.position;
        padPosition.y = transform.position.y;

        transform.position = padPosition;
    }

    #endregion
}