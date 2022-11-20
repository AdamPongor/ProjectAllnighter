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

    public void Upgrade()
    {
        level++;
    }

    public int GetDamage()
    {
        return baseDamage * (int)(0.5 * level + 1) + scaling;
    }

}
