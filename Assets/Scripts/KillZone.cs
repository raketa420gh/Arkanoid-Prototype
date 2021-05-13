using System;
using UnityEngine;


public class KillZone : MonoBehaviour
{
    #region Events

    public static event Action OnBallEnterKillZone;

    #endregion


    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.Ball))
        {
            OnBallEnterKillZone?.Invoke();
        }
    }

    #endregion
}