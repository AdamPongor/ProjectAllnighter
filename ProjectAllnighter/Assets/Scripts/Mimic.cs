using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : Enemy
{
    Vector2 lastdir;
    private float moveSpeed = 60f;
    private bool awake = false;
    private GameObject Player;

    private void FixedUpdate()
    {

        animator.SetFloat("AnimMoveX", lastdir.x);
        animator.SetFloat("AnimMoveY", lastdir.y);
        animator.SetBool("isMoving", false);
        if (awake)
        {
            if (!stunned)
            {
                animator.SetBool("isMoving", true);
                Vector2 dir = (Player.transform.position - transform.position).normalized;
                rb.AddForce(Time.deltaTime * moveSpeed * dir, ForceMode2D.Impulse);
                animator.SetFloat("AnimMoveX", dir.x);
                animator.SetFloat("AnimMoveY", dir.y);
                lastdir = dir;
                return;
            }
        }
    }

    public override void ResetPosition()
    {
        base.ResetPosition();
        awake = false;
        animator.SetBool("isMoving", false);
    }

    public override void Respawn()
    {
        base.Respawn();
        awake = false;
        animator.SetBool("isMoving", false);
    }

    public override int GetDamage()
    {
        if (!awake)
        {
            return 0;
        } else
        {
            return damage;
        }
    }

    public void Awaken()
    {
        awake = true;
        rb.mass = 1;
    }

    public override void takeDamage(float damage, PlayerData player)
    {
        Player = player.gameObject;
        base.takeDamage(damage, player);
        animator.SetBool("isMoving", true);
    }
}
