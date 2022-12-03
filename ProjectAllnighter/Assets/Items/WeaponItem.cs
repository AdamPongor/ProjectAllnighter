using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public GameObject weapon;

    public WeaponItem(GameObject weapon, PlayerData p) :
        base(false, ItemAssets.Instance.swordSprite, Item.ItemType.WEAPON, 1, p)
    {
        this.weapon = weapon;
    }

    public override bool Use()
    {
        
        GameObject wp = player.GetComponentInChildren<WeaponParent>().gameObject;
        GameObject w = Object.Instantiate(weapon, wp.transform.position, wp.transform.rotation);
        w.transform.SetParent(wp.transform, false);
        w.SetActive(false);
        player.GetComponent<PlayerController>().weapons.Add(w);
        return true;
    }
    public override Item Clone()
    {
        return new WeaponItem(weapon, player);
    }
}
