using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotion : Item
{
    public StaminaPotion(int amount, PlayerData p) :
        base(true, ItemAssets.Instance.staminaPotionSprite, Item.ItemType.STAMINAPOTION, amount, p)
    {

    }

    public override bool Use()
    {
        player.Stamina.BoostRegen(30);
        return true;
    }

    public override Item Clone()
    {
        return new StaminaPotion(amount, player);
    }
}
