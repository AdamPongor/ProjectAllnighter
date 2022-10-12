using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public bool IsAttacking { get; private set; }

    public SpriteRenderer characterRenderer;
    public SpriteRenderer weaponRenderer;
    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;

    private void Update() {
        if (IsAttacking)
            return;

        transform.right = Direction;

        if (Direction.y > 0)
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
}
