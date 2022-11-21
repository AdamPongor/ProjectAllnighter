using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{

    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    public Inventory(PlayerData player){
        itemList = new List<Item>();
        
        AddItem(new Item(false, ItemAssets.Instance.swordSprite, Item.ItemType.SWORD, 1, player));
        AddItem(new HealthPotion(1, player));
        AddItem(new Item(true, ItemAssets.Instance.staminaPotionSprite, Item.ItemType.STAMINAPOTION, 1, player));
        Debug.Log("items:" + itemList.Count);
    }

    public void AddItem(Item item){
        if(item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if(!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }

        }
        else
        {
            itemList.Add(item);
        }

        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }

    public void RemoveItem(Item item, int amount)
    {
        if(item.IsStackable())
        {
            Item itemInInventory = null;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= amount;
                    itemInInventory = inventoryItem;
                }
            }
            if(itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
            
            
        }
        else
        {
            itemList.Remove(item);
        }

        //itemList.Remove(item);

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
