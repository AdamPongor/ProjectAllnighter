using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private WeaponParent weaponParent;


    public enum PlayerStates{
        IDLE,
        WALK,
        DODGE
    }

    PlayerStates CurrentState{
        set{
            if(stateLock == false)
            {
                currentState = value;
            switch(currentState )
            {
                case PlayerStates.IDLE:
                    animator.Play("Idle");
                    canMove = true;
                    break;
                case PlayerStates.WALK:
                    animator.Play("Movement");
                    canMove = true;
                    break;
                case PlayerStates.DODGE:
                    
                    animator.Play("Dodge");
                    stateLock = true;
                    break;
            }
            }
        }

    }
    public float moveSpeed = 70f;
    public float maxSpeed = 8f;
    public float dodgeSpeed = 10f;
    //Each frame of physics, what percentage of the speed should be shaved off the velocity out of  1 (100%)
    public float idleFriction = 0.9f;
    Rigidbody2D rb;
    Animator animator;

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    
    Vector2 movementInput = Vector2.zero;
    Vector2 lastMoveDir = Vector2.zero;

    //Determine the switch case of the animations.
    PlayerStates currentState;
    // If this is true, animation state should not change.
    bool stateLock = false;
    // If this is false, player should not move.
    bool canMove = true;

    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponParent = GetComponentInChildren<WeaponParent>();
    }


    private void FixedUpdate(){

        
        if(canMove == true && movementInput != Vector2.zero)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);
        }
        if(movementInput != Vector2.zero)
            lastMoveDir = movementInput;

        

        if(currentState == PlayerStates.DODGE)
        {
            canMove = false;
            Vector2 dodgeDir = lastMoveDir;
            float decreaseDodgeSpeed = 4f;
            dodgeSpeed -= dodgeSpeed * decreaseDodgeSpeed * Time.deltaTime;
            rb.velocity = dodgeDir * dodgeSpeed;
        }
       
        
        

    }
    

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();


        // Update Animator for sprite direction
        if(movementInput != Vector2.zero)
        {
            CurrentState = PlayerStates.WALK;
            animator.SetFloat("MoveX",movementInput.x);
            animator.SetFloat("MoveY", movementInput.y);
        }
        else
        {
            CurrentState = PlayerStates.IDLE;
        }

        //update weapon position for the right direction
        weaponParent.Direction = getDirection();


    }

    void OnDodge()
    {
        CurrentState = PlayerStates.DODGE;
        
    }

    void OnDodgeFinished()
    {
        stateLock = false;
        if(movementInput != Vector2.zero)
            CurrentState = PlayerStates.WALK;
        else
            CurrentState = PlayerStates.IDLE;
        dodgeSpeed = 10;
    }
    void onFire()
    {
        print("Fire pressed");
    }


    Vector2 getDirection() 
    {
        return lastMoveDir;
    }
}
