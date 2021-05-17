using System;
using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables

    [Header("Movement Limit")] 
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private bool isControlOn;
    private Ball ball;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        isControlOn = true;
    }

    private void OnEnable()
    {
        PauseManager.OnPausedOn += PauseManagerOnPausedOn;
    }

    private void Update()
    {
        if (!isControlOn)
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


    #region Event Handlers

    private void PauseManagerOnPausedOn(bool isActive)
    {
        isControlOn = !isActive;
    }

    #endregion
}