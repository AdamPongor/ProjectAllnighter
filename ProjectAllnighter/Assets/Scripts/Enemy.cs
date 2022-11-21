using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class Enemy : MonoBehaviour
{

    public float health;
    public int XP;
    public int damage;
    private bool canDamage = true;
    protected bool stunned;
    [SerializeField] FloatingText floatingText;
    public float Health {
        get { return health; }

        private set
        {
            health = value;
        }
    }

    public void takeDamage(float damage, PlayerData player)
    {
        Health -= damage;
        FloatingText text = Instantiate(floatingText);
        text.Text = damage.ToString();
        RectTransform textTransform = text.GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textTransform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
        if (health <= 0)
        {
             Die(player);
        }
    }

    public void Die(PlayerData player)
    {
        player.AddXP(XP);
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
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

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerData player = collision.collider.GetComponent<PlayerData>();
            if (canDamage && !stunned)
            {
                player.takeDamage(damage);
                canDamage = false;
                Thread th = new Thread(resetCanDamage);
                th.Start();
            }
            
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        canDamage = true;
    }

    private void resetCanDamage()
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
