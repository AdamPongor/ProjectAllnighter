using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeaponData : MonoBehaviour
{
    public GameObject player;
    public string displayName;
    public int baseDamage;
    private int scaling;
    private int level = 0;
    private int maxLevel = 10;
    public UnityEvent Scale;
    public UnityEvent Attack;
    private bool equipped = false;

    public int Level { get => level; set{if (level<maxLevel) level = value; }}
    public int MaxLevel { get => maxLevel;}
    public bool Equipped { get => equipped; set => equipped = value; }

    public void Upgrade()
    {
        if (player.GetComponent<PlayerController>().Pay(getUpgradeCost())){
            Level++;
        };
    }

    public int GetDamage()
    {
        Scale?.Invoke();
        return baseDamage + (int)(baseDamage * 0.5 * Level) + (int) (baseDamage * Math.Log(scaling));
    }

    public int getUpgradeCost()
    {
        return (level + 1) * 10;
    }

    public bool UpgradeAble()
    {
        return level < maxLevel;
    }

    public void ScaleWithStrength()
    {
        scaling = player.GetComponent<PlayerData>().Strength;
    }

    public void ScaleWithIntelligence()
    {
        scaling = player.GetComponent<PlayerData>().Intelligence;
    }
    public void ScaleWithDexterity()
    {
        scaling = player.GetComponent<PlayerData>().Dexterity;
    }
}
