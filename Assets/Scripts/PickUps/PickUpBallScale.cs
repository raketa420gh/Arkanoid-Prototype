using UnityEngine;

public class PickUpBallScale : PickUpBase
{
    #region Variables

    [SerializeField] private float scaleСoefficient;

    private Ball ball;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
    }

    #endregion


    #region Protected methods

    protected override void ApplyEffect()
    {
        ball.gameObject.transform.localScale *= scaleСoefficient;
        Debug.Log($"Размер мяча стал {ball.gameObject.transform.localScale}.");
    }

    #endregion
}