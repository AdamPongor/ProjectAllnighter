using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    
    [HideInInspector] public Item item;
     public int amount = 1;
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    private Inventory inventory;
    public InventoryManager invManager;

    public PlayerController player;
    public event EventHandler OnItemClicked;
    
    

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite= newItem.itemSprite;
        amount = newItem.amount;
        invManager = null;
        player = null;
        RefreshAmount();
    }

    public void RefreshAmount()
    {
        this.GetComponentInChildren<TMPro.TextMeshProUGUI>().text =amount.ToString();
    }

    
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(item.itemType== Item.ItemType.CONSUMABLE)
            {
             if(item.Use())
             {
                
                this.amount--; 
                inventory.RemoveItem(item,1);
                if(this.amount<= 0)
                {
                    Destroy(this.gameObject);
                    invManager.RefreshInventoryItems(); 
                }
                invManager.RefreshInventoryItems();            
            }
            }
        }else if(eventData.button == PointerEventData.InputButton.Right)
        {
            Item duplicateItem = item.Clone();
            inventory.RemoveItem(item, item.amount);
            Destroy(this.gameObject);
            invManager.RefreshInventoryItems();
            ItemWorld.DropItem(player.GetPosition(),duplicateItem, player.LastMoveDir, 0.3f);
            // invManager.RefreshInventoryItems();
            // OnItemClicked?.Invoke(this,EventArgs.Empty);
        }
        
        
    }
}
