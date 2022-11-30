using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using UnityEngine.UIElements;
using CodeMonkey.Utils;
using Random = System.Random;

public class Enemy : MonoBehaviour
{

    public float maxHealth;
    public int XP;
    public int damage;
    protected bool canDamage = true;
    protected bool stunned;
    private Vector3 startingPos;
    private float health;
    [SerializeField] FloatingText floatingText;
    protected Rigidbody2D rb;
    protected Animator animator;
    public float Health {
        get { return health; }

        private set
        {
            health = value;
        }
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startingPos = gameObject.transform.position;
        Health = maxHealth;
    }

    public virtual void takeDamage(float damage, PlayerData player)
    {
        Health -= damage;
        FloatingText text = Instantiate(floatingText);
        text.Text = damage.ToString();
        RectTransform textTransform = text.GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textTransform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
        if (Health <= 0)
        {
             Die(player);
        }
    }

    public void Die(PlayerData player)
    {
        DropMoney();
        player.AddXP(XP);
        gameObject.SetActive(false);
    }

    private void DropMoney()
    {
        Random rnd = new Random();
        Item item = new Coin(rnd.Next(0, 5));
        Vector2 Dir = UtilsClass.GetRandomDir();
        ItemWorld.DropItem(gameObject.transform.position, item, Dir, 0);
    }

    public virtual void ResetPosition()
    {
        gameObject.transform.position = startingPos;
    }

    public virtual void Respawn()
    {
        canDamage = true;
        stunned = false;
        ResetPosition();
        Health = maxHealth;
        gameObject.SetActive(true);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerData player = collision.collider.GetComponent<PlayerData>();
            if (player.GetComponentInChildren<Weapon>().IsBlocking)
            {
                player.UseStamina(damage);
                Stun(collision.collider.GetComponent<PlayerController>().GetDamage());
            }
        }
    }

    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerData player = collision.collider.GetComponent<PlayerData>();
            if (canDamage && !stunned)
            {
                player.takeDamage(GetDamage());
                canDamage = false;
                Thread th = new Thread(resetCanDamage);
                th.Start();
            }
            
        }
    }

    public virtual int GetDamage()
    {
        return damage;
    }

    public virtual void resetCanDamage()
    {
        Thread.Sleep(1000);
        canDamage = true;
    }

    public void Stun(int time){
        stunned = true;
        Thread th = new Thread(() => resetStun(time));
        th.Start();
    }

    private void resetStun(int time){
        Thread.Sleep(time);
        stunned = false;
    }
}
