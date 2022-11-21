using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public StatusBar Stamina;
    public StatusBar Mana;
    public StatusBar Health; 
    public FloatingText floatingText;

    public GameObject lastInteracted;
    public List<GameObject> visitedBonfires = new List<GameObject>();
    

    public int XP = 100000;
    public int Level = 1;

    public int Vitality = 1;
    public int Endurance = 1;
    public int Strength = 1;
    public int Dexterity = 1;
    public int Intelligence = 1;

    public GameObject GetLastInteracted() { 
        return lastInteracted; 
    }
    public void SetLastInteracted(GameObject go)
    {
        lastInteracted = go;
    }


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

    public void AddBonfire(GameObject b)
    {
        if (!visitedBonfires.Contains(b))
        {
            visitedBonfires.Add(b);
        }
    }

    public List<GameObject> GetBonfires()
    {
        return visitedBonfires;
    }

    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
    }

    public void AddXP(int xp){
        XP += xp;
        FloatingText text = Instantiate(floatingText);
        text.SetColor(new Color(0, 8, 1, 1));
        text.Text = "+" + xp.ToString() + " XP";
        RectTransform textTransform = text.GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textTransform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
    }
}
