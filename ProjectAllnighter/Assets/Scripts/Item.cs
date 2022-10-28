using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType{
        SWORD,
        HEALTHPTION,
        MANAPOTION,

    }

    public ItemType itemType;
    public int amount;

}
