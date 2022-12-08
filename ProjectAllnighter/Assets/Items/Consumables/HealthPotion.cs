using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{

    public HealthPotion(int amount, PlayerData p) : 
        base(true, ItemAssets.Instance.healthPotionSprite, Item.ItemType.CONSUMABLE, amount, p)
    {

    }

    public override bool Use() {
        player.Heal(50);
        return true;
    }

    public override Item Clone()
    {
        return new HealthPotion(amount, player);
    }
}
