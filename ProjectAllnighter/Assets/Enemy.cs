using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float health = 30;
    public float Health {
        get { return health; }

        private set
        {
            health = value;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void takeDamage(float damage)
    {
        Health -= damage;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
