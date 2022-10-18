using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 8f;
     private Rigidbody2D projectilerb;

    // Start is called before the first frame update
    void Start()
    {
        projectilerb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        projectilerb.velocity = transform.right * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision is the object what the projectile hit
        Destroy(gameObject);
        Enemy d = collision.GetComponent<Enemy>();
        if (d != null)
        {
            d.takeDamage(10);
        }
    }
}
