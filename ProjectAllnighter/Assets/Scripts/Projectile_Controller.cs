using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 8f;
    private Rigidbody2D projectilerb;
    private float TTL = 3f;
    private float timeElapsed = 0.0f;
    public PlayerData Player { get; set; }
    public int Damage { get; set; }
    public bool fromPlayer { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        projectilerb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        projectilerb.velocity = transform.right * bulletSpeed;
        timeElapsed += Time.deltaTime;
        if (timeElapsed > TTL)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision is the object what the projectile hit
        if (fromPlayer)
        {
            Enemy d = collision.GetComponent<Enemy>();
            if (d != null)
            {
                d.takeDamage(Damage, Player);
                Destroy(gameObject);
            }
        }
        else
        {
            PlayerData p = collision.GetComponent<PlayerData>();
            if (p != null)
            {
                if (p.GetComponentInChildren<Weapon>().IsBlocking)
                {
                    p.UseStamina(Damage);
                    fromPlayer = true;
                    Player = p;
                    bulletSpeed *= -1;
                } else
                {
                    p.takeDamage(Damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
