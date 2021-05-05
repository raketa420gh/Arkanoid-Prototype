using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    #region Variables

    [SerializeField] private Text pointsText;
    [Min(0)] private int currentPoints;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        currentPoints = 0;
        UpdatePointsLabel();
    }

    private void OnEnable()
    {
        Block.OnDestroyBlock += AddPoints;
    }

    private void OnDisable()
    {
        Block.OnDestroyBlock -= AddPoints;
    }

    #endregion


    #region Private methods

    private void AddPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;
        UpdatePointsLabel();
    }

    private void UpdatePointsLabel()
    {
        pointsText.text = currentPoints.ToString();
    }

    #endregion
}