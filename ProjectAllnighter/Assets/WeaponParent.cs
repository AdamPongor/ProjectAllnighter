using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    //weapondirection set
    public Vector2 Direction { get; set; }
    public bool IsAttacking { get; private set; }

    //animation
    public SpriteRenderer characterRenderer;
    public SpriteRenderer weaponRenderer;
    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;
    private bool swordOnlLeft = false;

    //create weapon holders
    private int weaponcnt;
    public int currentWeaponIndex;

    public GameObject[] weapons;
    public GameObject currentWeapon;
    public GameObject weaponParent;

    //hitscan
    public Transform circleOrigin;
    public float radius;

    //projectile
    private bool isRanged;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;


    private void Start()
    {

        weaponcnt = 2;
        weapons = new GameObject[weaponcnt];


        for (int i = 0; i < weaponcnt; i++)
        {
            weapons[i] = weaponParent.transform.GetChild(i).gameObject;
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
        currentWeapon = weapons[0];
        weaponcnt = 0;
    }
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
        }
        else if ((Direction.x > 0 || Direction.y > 0) && swordOnlLeft) 
        {
            Vector3 swordpos = transform.localPosition;
            swordpos.x *= -1;
            transform.localPosition = swordpos;
            swordOnlLeft = false;
            //mirror sword
            transform.Rotate(0, 180, 0);
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
    private void RangedAttack()
    {
        animator.SetTrigger("Attack");
        Instantiate(projectile, firePoint.position, firePoint.rotation);
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());

    }

    public void ChangeWeapon()    
    {
        //change weapon to key R

        if (currentWeaponIndex == 0)
        {
            weapons[currentWeaponIndex].SetActive(false);
            weapons[1].SetActive(true);
            currentWeapon = weapons[1];
            currentWeaponIndex = 1;
            isRanged = true;
        }
        else
        {
            weapons[currentWeaponIndex].SetActive(false);
            weapons[0].SetActive(true);
            currentWeapon = weapons[0];
            currentWeaponIndex = 0;
            isRanged = false;
        }
        //change renderers
        weaponRenderer = currentWeapon.GetComponent<SpriteRenderer>();
        animator = currentWeapon.GetComponent<Animator>();
    }
    public void Attack()
    {
        if (attackBlocked)
            return;
        if (isRanged)
        {
            RangedAttack();
            return;
        }
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
