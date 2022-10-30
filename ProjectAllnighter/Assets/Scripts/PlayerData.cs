using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public StatusBar Stamina;
    public StatusBar Mana;
    public StatusBar Health;

    private List<Bonfire> visitedBonfires = new List<Bonfire>();

    public void UseStamina(float amount)
    {
        Stamina.Use(amount);
    }

    public bool isEnoughStamina(float amount)
    {
        return Stamina.isEnough(amount);
    }
    public void UseMana(float amount)
    {
        Mana.Use(amount);
    }

    public bool isEnoughMana(float amount)
    {
        return Mana.isEnough(amount);
    }

    public void takeDamage(float amount)
    {
        Health.Use(amount);
    }

    public void Heal(float amount)
    {
        Health.Add(amount);
    }

    public void AddBonfire(Bonfire b)
    {
        visitedBonfires.Add(b);
    }
}
