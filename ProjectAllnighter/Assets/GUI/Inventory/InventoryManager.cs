using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    private Inventory inventory;
    public GameObject inventoryItemPrefab;
    public PlayerController player;
    
    
    public void AddItem(Item item)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null && itemInSlot.item.Equals(item))
            {
                itemInSlot.amount = item.amount;
                return ;
            } 
        }

        for(int i = 0; i < inventorySlots.Length; i++)
        {
            
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(item,slot);
                return ;
            } 
        }
        return ;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab,slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
        inventoryItem.invManager = this;
        inventoryItem.SetInventory(inventory);
        inventoryItem.SetPlayer(this.player);

    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
               
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        foreach(Item item in inventory.GetItemList())
        {
            bool itemIsInList = false;   
            
            for(int i = 0; i < inventorySlots.Length;i++)
            {
                if(inventorySlots[i].GetComponentInChildren<InventoryItem>() != null)
                {
                    if(inventorySlots[i].GetComponentInChildren<InventoryItem>().item.Equals(item) )
                    {
                        itemIsInList = true;  
                        break;
                    }
                }
                
                
            }
            if(itemIsInList == false)
            {
                for(int i = 0; i < inventorySlots.Length;i++)
                {
                    if(inventorySlots[i].GetComponentInChildren<InventoryItem>() == null)
                    {
                        AddItem(item);
                        break;
                    }
                }
                
            }

            if(itemIsInList)
            {
                for(int i = 0; i < inventorySlots.Length; i++)
                {
                    if(inventorySlots[i].GetComponentInChildren<InventoryItem>() != null &&
                      inventorySlots[i].GetComponentInChildren<InventoryItem>().item.Equals(item) &&
                      inventorySlots[i].GetComponentInChildren<InventoryItem>().item.IsStackable())
                    {
                        AddItem(item);
                        inventorySlots[i].GetComponentInChildren<InventoryItem>().RefreshAmount();
                    }
                }
            }
   

            
        } 
    }

    
    


    
 
}
