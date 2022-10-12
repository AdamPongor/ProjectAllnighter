using System.Collections;
using System.Collections.Generic;
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

    public Transform circleOrigin;
    public float radius;
    private Vector3 pos;

    private void Update() {

        if (IsAttacking)
            return;



        //setting the direction towards where the character is facing
        transform.right = Direction;
        lastDirection = Direction;

        if (Direction.y > 0)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
        
        //setting the position of the weapon closer to the sideways character sprite
        pos = transform.position;
        //TODO ez a feltetel nem jo... egyszer kell csak arrebb rakni ennyivel
        if (!lastDirection.Equals(Direction))
        {
            if (Direction.x > 0)
            {
                pos.x -= 0.1f;
            }
            else if (Direction.x < 0)
            {
                pos.x += 0.1f;
            }
            transform.position = pos;
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
