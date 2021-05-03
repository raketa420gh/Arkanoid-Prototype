using UnityEngine;

public class Block : MonoBehaviour
{
    #region Variables

    public delegate void OnDestroyBlockDelegate(int rewardPoints);

    public static event OnDestroyBlockDelegate OnDestroyBlock;

    [SerializeField] [Min(0)] private int maxDurability;
    [SerializeField] [Min(0)] private int awardPoints;
    [SerializeField] private Sprite damagedSprite;

    private SpriteRenderer spriteRenderer;
    private int currentDurability;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        RestoreDurability();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentDurability--;

        if (currentDurability < maxDurability)
        {
            SetDamagedSprite();
        }

        if (currentDurability == 0)
        {
            OnDestroyBlock?.Invoke(awardPoints);
            Destruct();
        }
    }

    #endregion


    #region Private Methods

    private void Destruct()
    {
        Destroy(gameObject);
    }

    private void RestoreDurability()
    {
        currentDurability = maxDurability;
    }

    private void SetDamagedSprite()
    {
        spriteRenderer.sprite = damagedSprite;
    }

    #endregion
}