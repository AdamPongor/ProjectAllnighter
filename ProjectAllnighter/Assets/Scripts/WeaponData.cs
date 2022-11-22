using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public GameObject player;
    public string displayName;
    public int baseDamage;
    public int scaling;
    private int level = 0;
    private int maxLevel = 10;

    public int Level { get => level; set{if (level<maxLevel) level = value; }}
    public int MaxLevel { get => maxLevel;}

    public void Upgrade()
    {
        if (player.GetComponent<PlayerController>().Pay(getUpgradeCost())){
            Level++;
        };
    }

    public int GetDamage()
    {
        return baseDamage + (int)(baseDamage * 0.5 * Level) + scaling;
    }

    public int getUpgradeCost()
    {
        return (level + 1) * 10;
    }

    public bool UpgradeAble()
    {
        return level < maxLevel;
    }
}
