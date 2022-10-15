using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    private Vector2 lastDirection;
    public bool IsAttacking { get; private set; }

    public SpriteRenderer characterRenderer;
    public SpriteRenderer weaponRenderer;
    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;
    private bool swordOnlLeft= false;


    public Transform circleOrigin;
    public float radius;

    private void Update() {

        if (IsAttacking)
            return;

        //setting the sword's direction towards where the character is facing
        if ((Direction.x < 0 || Direction.y < 0) && !swordOnlLeft)
        {
            Vector3 swordpos = transform.localPosition;
            swordpos.x *= -1;
            transform.localPosition = swordpos;
            swordOnlLeft = true;
            //mirror sword
            transform.Rotate(0,180,0);
            lastDirection = Direction;
        }
        else if ((Direction.x > 0 || Direction.y > 0) && swordOnlLeft) 
        {
            Vector3 swordpos = transform.localPosition;
            swordpos.x *= -1;
            transform.localPosition = swordpos;
            swordOnlLeft = false;
            //mirror sword
            transform.Rotate(0, 180, 0);
            lastDirection = Direction;
        }

        //setting sorting order in case of moving upwards or downwards
        if (Direction.y > 0 || Direction.x < 0)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }

    }
    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
    public void ResetIsAttacking() { 
        IsAttacking = false;
    }
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position,radius);
    }
    public void DetectColliders() 
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            Debug.Log(collider.name);
        }
    }
}
