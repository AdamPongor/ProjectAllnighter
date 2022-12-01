using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    Vector2 lastdir;
    public DetectionZone zone;
    private float moveSpeed = 40f;

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
}
