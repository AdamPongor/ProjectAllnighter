using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class Item
{
    public enum ItemType {
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
    protected PlayerData player;

    public Item(bool stackabe, Sprite sprite, ItemType type, int amount, PlayerData player) {
        Stackable = stackabe;
        itemSprite = sprite;
        itemType = type;
        this.amount = amount;
        this.player = player;
    }

    public virtual void Use()
    {
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
