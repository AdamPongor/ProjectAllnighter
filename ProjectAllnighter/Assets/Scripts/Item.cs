using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Item 
{
    public enum ItemType{
        SWORD,
        HEALTHPOTION,
        MANAPOTION,
        STAMINAPOTION,
        COIN,

    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.SWORD:            return ItemAssets.Instance.swordSprite;
            case ItemType.STAMINAPOTION:    return ItemAssets.Instance.staminaPotionSprite;
            case ItemType.HEALTHPOTION:     return ItemAssets.Instance.healthPotionSprite;
            case ItemType.MANAPOTION:       return ItemAssets.Instance.manaPotionSprite;
            case ItemType.COIN:             return ItemAssets.Instance.coinSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.COIN:
            case ItemType.HEALTHPOTION:
            case ItemType.MANAPOTION:
            case ItemType.STAMINAPOTION:
                return true;
            case ItemType.SWORD:
                return false;
        }
    }

}
