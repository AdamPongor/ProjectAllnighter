using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public GameObject weapon;
    public WeaponItem(GameObject weapon, Sprite s ,PlayerData p) :
        base(false, s, Item.ItemType.WEAPON, 1, p)
    {
        this.weapon = weapon;
    }

    public override bool Use()
    {
        if (weapon.GetComponent<WeaponData>().Equipped)
        {
            Unequip();
        } else
        {
            weapon.GetComponent<WeaponData>().Equipped = true;
        }
        return false;
    }
    public void Unequip()
    {
        weapon.GetComponent<WeaponData>().Equipped = false;
        weapon.SetActive(false);
    }

    public override Item Clone()
    {
        return new WeaponItem(weapon, itemSprite, player);
    }
}
