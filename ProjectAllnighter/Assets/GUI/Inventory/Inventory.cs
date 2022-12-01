using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory 
{

    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    public Inventory(PlayerData player){
        itemList = new List<Item>();
        
        AddItem(new ManaPotion(1, player));
        // AddItem(new HealthPotion(1, player));
        // AddItem(new StaminaPotion(1, player));
        AddItem(new Item(false, ItemAssets.Instance.swordSprite, Item.ItemType.WEAPON,1, player));
        Debug.Log("items:" + itemList.Count);
    }

    public void AddItem(Item item){
        if(item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemSprite == item.itemSprite)
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
        itemList[itemList.IndexOf(item)].amount -= amount;
        if (itemList[itemList.IndexOf(item)].amount == 0) {
            itemList.Remove(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }


    public bool RemoveItemType(Item.ItemType type, int amount)
    {
        Item itemInInventory = null;
        bool retValue = false;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == type)
            {
                if (inventoryItem.amount >= amount)
                {
                    inventoryItem.amount -= amount;
                    itemInInventory = inventoryItem;
                    retValue = true;
                } else
                {
                    return false;
                }
            }
        }
        if (itemInInventory != null && itemInInventory.amount <= 0)
        {
            itemList.Remove(itemInInventory);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

        return retValue;
    }
    

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
