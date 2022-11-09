using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    

    //weapondirection set
    public Vector2 Direction { get; set; }
    public bool IsAttacking { get; protected set; }
    public bool IsBlocking { get; protected set; }

    //animation
    public SpriteRenderer characterRenderer;
    public SpriteRenderer weaponRenderer;
    public Animator animator;
    public float delay = 0.3f;
    protected bool attackBlocked;
    private bool swordOnlLeft = true;

    //hitscan
    public Transform circleOrigin;
    public float radius;

    //projectile
    private bool isRanged;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    protected Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    public void Update() {

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mousePosition.x - Screen.width / 2, mousePosition.y - Screen.height / 2);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        if (IsAttacking || gameObject.GetComponentInParent<PlayerController>().inMenu)
            return;

        animator.SetFloat("AnimX",Direction.x);
        animator.SetFloat("AnimY", Direction.y);

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
    public void RangedAttack()
    {
        //calculate the projectiles vector
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        Instantiate(projectile, firePoint.position, firePoint.rotation);
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());

    }

    public void ChangeWeapon(GameObject currentWeapon)    
    {
        weaponRenderer = currentWeapon.GetComponent<SpriteRenderer>();
        animator = currentWeapon.GetComponent<Animator>();
    }
    public virtual void MeleeAttack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    public virtual void Defend()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Defend");
        IsAttacking = true;
        IsBlocking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    public IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
    public void ResetIsAttacking() { 
        IsAttacking = false;
    }

    public void ResetIsBlocking()
    {
        IsBlocking = false;
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
            
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().takeDamage(10);
            }
        }
    }
}
