using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    public StatusBar Stamina;
    public StatusBar Mana;
    public StatusBar Health;
    public FloatingText floatingText;

    public GameObject lastInteracted;
    public List<GameObject> visitedBonfires = new List<GameObject>();
    public UnityEvent DeathEvent;


    public int XP = 100000;
    public int Level = 1;

    private int vitality = 1;
    private int endurance = 1;
    private int strength = 1;
    private int dexterity = 1;
    private int intelligence = 1;

    //flash controller here because damage detection as well 

    private SimpleFlash flash;
    

    public int Vitality
    {
        get => vitality;
        set
        {
            vitality = value;
            Health.MaxValue += (vitality - 1) * 5;
        }
    }
    public int Endurance
    {
        get => endurance;
        set
        {
            endurance = value;
            Stamina.MaxValue += (endurance - 1) * 3;
        }
    }
    public int Strength { get => strength; set => strength = value; }
    public int Dexterity { get => dexterity; set => dexterity = value; }
    public int Intelligence
    {
        get => intelligence;
        set
        {
            intelligence = value;
            Mana.MaxValue += (intelligence - 1) * 3;
        }
    }

    public GameObject GetLastInteracted() {
        return lastInteracted;
    }
    public void SetLastInteracted(GameObject go)
    {
        lastInteracted = go;
    }

    void Start()
    {
        flash = GetComponent<SimpleFlash>();
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
        if (poisoned != null)
        {
            flash.Flash(new Color(0.7f,0,0.7f));
        } else
        {
            flash.Flash(Color.red);
        }
        if (Health.currentValue <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        XP = 0;
        StopPoison();
        DeathEvent?.Invoke();
    }

    public void Respawn()
    {
        if (lastInteracted != null)
        {
            Teleport(lastInteracted.transform.position + new Vector3(0.0f, 0.3f, 0.0f));
        } else {
            Teleport(new Vector3(0, 0, 0));
        }
            
        Health.Add(Health.MaxValue);
        Stamina.Add(Stamina.MaxValue);
        Mana.Add(Mana.MaxValue);
        
    }

    public void Heal(float amount)
    {
        StopPoison();
        flash.Flash(Color.green);
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

    private int damageSum = 0;
    private Coroutine poisoned = null;

    public void GetPoisened(int damage)
    {
        
        if (poisoned == null)
        {
            poisoned = StartCoroutine("PoisonDamage", damage);
        }
    }

    public void StopPoison()
    {
        StopCoroutine("PoisonDamage");
        damageSum = 0;
        poisoned = null;
    }
    
    IEnumerator PoisonDamage(int damage)
    {
        while (damageSum < damage)
        {
            takeDamage(1);
            damageSum += 1;
            yield return new WaitForSeconds(0.2f);
        }
        damageSum = 0;
        poisoned = null;
    }
}
