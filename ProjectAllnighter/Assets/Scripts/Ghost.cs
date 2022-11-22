using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    Vector2 lastdir;
    public DetectionZone zone;
    private float moveSpeed = 40f;
    Rigidbody2D rb;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        animator.SetFloat("AnimMoveX", lastdir.x);
        animator.SetFloat("AnimMoveY", lastdir.y);
        animator.SetBool("isMoving", false);
        if (zone.detectedObjs.Count > 0)
        {

            Collider2D detected = zone.detectedObjs[0];
            float distance = Vector3.Distance(detected.transform.position, this.transform.position);
            if (detected && !stunned)
            {
                if (distance > 1.25)
                {
                    animator.SetBool("isMoving", true);
                    Vector2 dir = (detected.transform.position - transform.position).normalized;
                    rb.AddForce(Time.deltaTime * moveSpeed * dir, ForceMode2D.Impulse);
                    animator.SetFloat("AnimMoveX", dir.x);
                    animator.SetFloat("AnimMoveY", dir.y);
                    lastdir = dir;
                    return;
                }
                else {
                    Attack();
                    return;
                }
            }
        }
    }
    private void Attack() {
    
    }
}
