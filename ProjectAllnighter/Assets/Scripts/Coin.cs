using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public Coin(int amount) : base(true, ItemAssets.Instance.coinSprite, Item.ItemType.COIN, amount, null)
    {
    }
}
