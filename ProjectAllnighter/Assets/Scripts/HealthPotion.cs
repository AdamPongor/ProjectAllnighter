using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    public HealthPotion(int amount, PlayerData p) : 
        base(true, ItemAssets.Instance.healthPotionSprite, Item.ItemType.HEALTHPOTION, amount, p)
    {

    }

    public override void Use() {
        player.Heal(20);
    }
}
