using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemWorldSpawner : MonoBehaviour
{
    public UnityEvent SetItem;
    public PlayerData player;
    private Item item;

    private void Awake()
    {
        SetItem?.Invoke();
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }

    public void HealthPotion()
    {
        item = new HealthPotion(1, player);
    }
    public void StaminaPotion()
    {
        item = new StaminaPotion(1, player);
    }
    public void ManaPotion()
    {
        item = new ManaPotion(1, player);
    }
    public void Coin()
    {
        item = new Coin(100);
    }
    public void Sword()
    {
        item = new Item(false, ItemAssets.Instance.swordSprite, Item.ItemType.WEAPON, 1, player);
    }
}
