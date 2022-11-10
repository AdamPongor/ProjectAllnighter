using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    public DetectionZone zone;
    private float moveSpeed = 60f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Collider2D detected = zone.detectedObjs[0];
        if (detected)
        {
            Vector2 dir = (detected.transform.position - transform.position).normalized;
            rb.AddForce(dir*moveSpeed*Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
