using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance{get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;
    
    public Sprite swordSprite;
    public Sprite wandSprite;
    public Sprite shieldSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite staminaPotionSprite;
    public Sprite coinSprite;
}
