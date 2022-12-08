using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using UnityEditor.UIElements;

public class UI_InventoryBar : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private PlayerController player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }


    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate)   continue;
            Destroy(child.gameObject);
        }
        int x = -2;
        float itemSlotCellSize = 64f;
        foreach(Item item in inventory.GetItemList())
        {
            if(x == 3)
            {
                break;
            }
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate,itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                //Use item
                if (item.Use())
                {
                    inventory.RemoveItem(item, 1);
                }
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                //Drop item
                Item duplicateItem = item.Clone();//new Item(item.Stackable, item.itemSprite, item.itemType, item.amount, player.GetComponent<PlayerData>());
                
                inventory.RemoveItem(item, item.amount);
                ItemWorld.DropItem(player.GetPosition(),duplicateItem, player.LastMoveDir, 0.3f);
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1 )
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText(" ");
            }
            x++;
            
            
        }
    }
}
