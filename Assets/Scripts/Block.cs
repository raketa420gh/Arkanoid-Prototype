using UnityEngine;

public class Block : MonoBehaviour
{
    #region Variables

    [Header("Settings")]
    [SerializeField] [Min(0)] private int maxDurability;
    [SerializeField] [Min(0)] private int awardPoints;

    [Header("Sprite Settings")] 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite damagedSprite;

    private bool isDamaged;
    private int currentDurability;

    #endregion


    #region Events

    public delegate void OnDestroyBlockDelegate(int rewardPoints);

    public static event OnDestroyBlockDelegate OnDestroyBlock;

    //уточнить, как правильно Action использовать в качестве альтернативы delegate
    //public static event Action<int> OnDestroyBlock1;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        RestoreDurability();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentDurability--;

        if (currentDurability < maxDurability)
        {
            isDamaged = true;
            SetDamagedSprite();
            
            if (currentDurability == 0)
            {
                OnDestroyBlock?.Invoke(awardPoints);
                Destruct();
            }
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
        isDamaged = false;
    }

    private void SetDamagedSprite()
    {
        spriteRenderer.sprite = damagedSprite;
    }

    #endregion
}