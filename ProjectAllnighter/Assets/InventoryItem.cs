using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    
    [HideInInspector] public Item item;
     public int amount = 1;
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    
    

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite= newItem.itemSprite;
        
    }

    public void RefreshAmount()
    {
        this.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = amount.ToString();
        // bool textActive = amount > 1;
        // this.GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.SetActive(textActive);
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
}
