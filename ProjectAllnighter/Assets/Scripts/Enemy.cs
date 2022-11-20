using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Enemy : MonoBehaviour
{

    public float health;
    public int XP;
    public int stuntime;
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
            collision.collider.GetComponent<PlayerData>().takeDamage(25, this);
        }
    }

    public void Stun(){
        stunned = true;
        Thread th = new Thread(resetStun);
        th.Start();
    }

    private void resetStun(){
        Thread.Sleep(stuntime);
        stunned = false;
    }
}
