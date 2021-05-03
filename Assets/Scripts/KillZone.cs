using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    #endregion
}