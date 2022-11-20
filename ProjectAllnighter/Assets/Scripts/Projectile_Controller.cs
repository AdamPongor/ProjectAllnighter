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
        
        Enemy d = collision.GetComponent<Enemy>();
        if (d != null)
        {
            d.takeDamage(10, Player);
            Destroy(gameObject);
        }
    }
}
