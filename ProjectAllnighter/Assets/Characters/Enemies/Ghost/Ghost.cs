using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    Vector2 lastdir;
    public DetectionZone zone;
    private float moveSpeed = 40f;
    private bool attackBlocked;
    public float attackPerSecond;


    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    private void FixedUpdate()
    {

        animator.SetFloat("AnimMoveX", lastdir.x);
        animator.SetFloat("AnimMoveY", lastdir.y);
        animator.SetBool("isMoving", false);
        if (zone.detectedObjs.Count > 0)
        {

            Collider2D detected = zone.detectedObjs[0];
            float distance = Vector3.Distance(detected.transform.position, this.transform.position);

            Vector2 offset = new Vector2(detected.transform.position.x - this.transform.position.x, detected.transform.position.y - this.transform.position.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);

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
                else if (distance < 0.5)
                {
                    animator.SetBool("isMoving", true);
                    Vector2 dir = (detected.transform.position - transform.position).normalized;
                    rb.AddForce((-1)*Time.deltaTime * moveSpeed * dir, ForceMode2D.Impulse);
                    animator.SetFloat("AnimMoveX", dir.x);
                    animator.SetFloat("AnimMoveY", dir.y);
                    lastdir = dir;
                    return;
                }
                else
                {
                    Attack();
                    return;
                }
            }
        }
    }

    public override void Respawn()
    {
        base.Respawn();
        attackBlocked = false;
    }

    private void Attack() {
        if (attackBlocked)
            return;
        GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Projectile_Controller pc = proj.GetComponent<Projectile_Controller>();
        pc.Damage = damage;
        pc.fromPlayer = false;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    public IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(1/attackPerSecond);
        attackBlocked = false;
    }
}
