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
        COIN
    }

    public bool Stackable;
    public Sprite itemSprite;
    public ItemType itemType;
    public int amount;

    public Item(bool stackabe, Sprite sprite, ItemType type, int amount){
        Stackable = stackabe;
        itemSprite = sprite;
        itemType = type;
        this.amount = amount;
    }

    

    public Sprite GetSprite()
    {
        return itemSprite;
    }

    public bool IsStackable()
    {
        return Stackable;
    }
}
