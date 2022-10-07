using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

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
                    canMove = false;
                    break;
            }
            }            
        }
        
    }
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;

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
    }

    
    private void FixedUpdate(){
        // Control animation parameters
        // if(!animLocked && movementInput != Vector2.zero)
        // {
        //     animator.SetFloat(animationStrings.moveX, movementInput.x);
        //     animator.SetFloat(animationSTrings.moveY, movementInput.y);
        // }
        if(movementInput != Vector2.zero){
            bool success = TryMove(movementInput);

            if(!success){
                success = TryMove(new Vector2(movementInput.x, 0));
                
                if(!success){
                    success = TryMove(new Vector2(movementInput.y,0));
                }
            }
            
        }
    }
    private bool TryMove(Vector2 direction){
        // Check for potential collisions
        int count = rb.Cast(
                direction, // Direction from the body to look fro collisions
                movementFilter, // Determine where a collision can occur
                castCollisions, // List of collisions to store after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset 
            );
            
            
            if(count == 0)
            {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
            }
            else{
                return false;
            }
    }
   
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();


        // Update Animator for sprite direction
        if(canMove && movementInput != Vector2.zero)
        {
            CurrentState = PlayerStates.WALK;
            animator.SetFloat("MoveX",movementInput.x);
            animator.SetFloat("MoveY", movementInput.y);
        }
        else
        {
            CurrentState = PlayerStates.IDLE;
        }
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
        
    }
}
