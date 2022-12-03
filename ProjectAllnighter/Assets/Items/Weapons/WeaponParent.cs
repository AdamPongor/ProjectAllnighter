using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponParent : MonoBehaviour
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

    //hitscan
    public Transform circleOrigin;
    public float radius;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    protected Camera mainCamera;
    private PlayerData playerData;

    private void Start()
    {
        mainCamera = Camera.main;
        playerData = GetComponentInParent<PlayerData>();
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
        if (playerData.isEnoughMana(20f))
        {
            //calculate the projectiles vector
            if (attackBlocked)
                return;
            animator.SetTrigger("Attack");
            GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
            Projectile_Controller pc = proj.GetComponent<Projectile_Controller>();
            pc.fromPlayer = true;
            PlayerController player = GetComponentInParent<PlayerController>();
            pc.Player = GetComponentInParent<PlayerData>();
            pc.Damage = player.GetDamage();
            IsAttacking = true;
            attackBlocked = true;
            StartCoroutine(DelayAttack());
            playerData.UseMana(20f);
        }
    }

    public virtual void MeleeAttack()
    {
        if (playerData.isEnoughStamina(20f))
        {
            if (attackBlocked)
                return;
            animator.SetTrigger("Attack");
            IsAttacking = true;
            attackBlocked = true;
            StartCoroutine(DelayAttack());
            playerData.UseStamina(20f);
        }
    }

    public virtual void Defend()
    {
        if (playerData.isEnoughStamina(20f))
        {
            if (attackBlocked)
                return;
            animator.SetTrigger("Defend");
            IsAttacking = true;
            IsBlocking = true;
            attackBlocked = true;
            StartCoroutine(DelayAttack());
        }
            
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
            
            if (collider.tag == "Enemy" && collider.isTrigger)
            {
                Enemy e = collider.GetComponent<Enemy>();
                PlayerData p = GetComponentInParent<PlayerData>();
                PlayerController pc = GetComponentInParent<PlayerController>();
                e.takeDamage(pc.GetDamage(), p);
            }
            if (collider.tag == "Destructable")
            {
                Barrel b = collider.GetComponent<Barrel>();
                b.Destroy();
            }
        }
    }
}
