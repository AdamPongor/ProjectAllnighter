using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float health = 30;
    [SerializeField] FloatingText floatingText;
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
        FloatingText text = Instantiate(floatingText);
        text.Text = damage.ToString();
        RectTransform textTransform = text.GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textTransform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
