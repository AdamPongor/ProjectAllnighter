using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Item
{
    public ManaPotion(int amount, PlayerData p) :
        base(true, ItemAssets.Instance.manaPotionSprite, Item.ItemType.CONSUMABLE, amount, p)
    {

    }

    public override bool Use()
    {
        player.Stamina.BoostRegen(60);
        return true;
    }

    public override Item Clone()
    {
        return new ManaPotion(amount, player);
    }
}
