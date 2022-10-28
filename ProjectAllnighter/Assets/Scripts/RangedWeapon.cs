using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : IAttack
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;

   /* public void Attack()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mousePosition.x - Screen.width / 2, mousePosition.y - Screen.height / 2);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        Instantiate(projectile, firePoint.position, firePoint.rotation);
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }*/
}
