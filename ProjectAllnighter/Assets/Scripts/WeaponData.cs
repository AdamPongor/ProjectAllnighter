using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public GameObject player;
    public string displayName;
    public int baseDamage;
    public int scaling;
    private int level = 0;

    public int Level { get => level; set => level = value; }

    public void Upgrade()
    {
        Level++;
    }

    public int GetDamage()
    {
        return baseDamage + (int)(baseDamage * 0.5 * Level) + scaling;
    }

}
