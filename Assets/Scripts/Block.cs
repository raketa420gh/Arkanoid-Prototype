using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    #region Variables

    [Header("Settings")] 
    [SerializeField] [Min(0)] private int maxDurability;
    [SerializeField] [Min(0)] private int awardPoints;
    [SerializeField] private bool isInvisibleAtStart;

    [Header("Pick Up Settings")] 
    [SerializeField] private GameObject[] pickUpsPrefabs;
    [Range(0, 100)] [SerializeField] private int pickUpCreationRate;

    [Header("Sprite Settings")] 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite damagedSprite;

    private bool isDamaged;
    private bool canDamaged = true;
    private int currentDurability;

    #endregion


    #region Events

    public static event Action<int> OnDestroyed;
    public static event Action OnCreated;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        RestoreDurability();

        if (isInvisibleAtStart)
        {
            SetVisibility(false);
            canDamaged = false;
        }
        
        OnCreated?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canDamaged)
        {
            ReduceDurability();
        }
        else
        {
            SetVisibility(true);
            canDamaged = true;
        }
    }

    #endregion


    #region Private Methods

    private void ReduceDurability()
    {
        currentDurability--;

        if (currentDurability < maxDurability)
        {
            isDamaged = true;
            SetDamagedSprite();

            if (currentDurability <= 0)
            {
                DestroyBlock();
            }
        }
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

    private void SetVisibility(bool isVisible)
    {
        spriteRenderer.color = isVisible ? Color.white : Color.clear;
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);

        if (IsNeedToCreatePickUp())
        {
            var randomPrefabNumber = Random.Range(0, pickUpsPrefabs.Length);
            Instantiate(pickUpsPrefabs[randomPrefabNumber], transform.position, Quaternion.identity);
        }
        OnDestroyed?.Invoke(awardPoints);
    }

    private bool IsNeedToCreatePickUp()
    {
        var randomNumber = Random.Range(1, 101);
        return pickUpCreationRate >= randomNumber;
    }

    #endregion
}