using System;
using UnityEngine;


public class KillZone : MonoBehaviour
{
    #region Events

    public static event Action OnBallEntered;

    #endregion


    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.Ball))
        {
            OnBallEntered?.Invoke();
        }
    }

    #endregion
}