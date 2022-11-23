using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class Item
{
    public enum ItemType {
        WEAPON,
        ARMOR,
        CONSUMABLE,
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

    public virtual Item Clone()
    {
        return new Item(Stackable,itemSprite,itemType,amount,player);
    }

    public virtual bool Use()
    {
        Debug.Log("Nyehehehe... Hé Loisz! Item lettem!");
        return false;
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
