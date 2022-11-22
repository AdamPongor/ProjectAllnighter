using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Slime : Enemy
{
    Vector2 lastdir;
    public DetectionZone zone;
    public float moveSpeed = 40f;
    Rigidbody2D rb;
    Animator animator;
    public int poisonDamage;

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
            if (detected && !stunned)
            {
                animator.SetBool("isMoving", true);
                Vector2 dir = (detected.transform.position - transform.position).normalized;
                rb.AddForce(Time.deltaTime * moveSpeed * dir, ForceMode2D.Impulse);
                animator.SetFloat("AnimMoveX", dir.x);
                animator.SetFloat("AnimMoveY", dir.y);
                lastdir = dir;
                return;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (!stunned)
            {
                PlayerData player = collision.collider.GetComponent<PlayerData>();
                player.GetPoisened(poisonDamage);
            }
        }
    }

    public override void resetCanDamage()
    {
        Thread.Sleep(200);
        canDamage = true;
    }
}
