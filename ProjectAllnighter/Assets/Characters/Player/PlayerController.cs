using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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
                    break;
            }
            }
        }
        get
        {
            return currentState;
        }
    }

    public Vector2 LastMoveDir { get => lastMoveDir; set => lastMoveDir = value; }

    public bool inMenu = false;
    public bool HoveringMenu { get; set; }
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
    [SerializeField] private UI_InventoryBar uI_InventoryBar;
    public InventoryManager inventoryManager;
    private bool inventoryOpen = false;
    public UnityEvent OpenInventory;
    public UnityEvent CloseInventory;
    //dynamic weapon list
    private WeaponParent weaponParent;
    public int currentWeaponIndex;
    public List<GameObject> weapons;
    public GameObject currentWeapon;

    public PlayerInput playerInput;

    private Inventory inventory;

    PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = gameObject.GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        
        
        inventory = new Inventory(GetComponent<PlayerData>());
        uI_InventoryBar.SetInventory(inventory);
        uI_InventoryBar.SetPlayer(this);
        inventoryManager.SetInventory(inventory);



        //dynamic weapon list
        weapons = new List<GameObject>();
        for (int i = 0; i < weaponParent.transform.childCount; i++)
        {
            weapons.Add(weaponParent.transform.GetChild(i).gameObject);
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
    }

    private void FixedUpdate(){

        //Move the player if it's moveable and there is any input.
        if(canMove == true && movementInput != Vector2.zero && !inMenu)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);
        }

        //Save the last input so if the player dodging while in IDLE state it will dodge to the direction of the last input.
        if(movementInput != Vector2.zero)
            LastMoveDir = movementInput;

        
        //While dodging lock the movement
        if(currentState == PlayerStates.DODGE)
        {
            canMove = false;
            Vector2 dodgeDir = LastMoveDir;
            float decreaseDodgeSpeed = 4f;
            dodgeSpeed -= dodgeSpeed * decreaseDodgeSpeed * Time.deltaTime;
            rb.velocity = dodgeDir * dodgeSpeed;
            
        }
        //set the weaponparent in case of weapon change
        weaponParent = GetComponentInChildren<WeaponParent>();
        //Update weapon position for the right direction
        weaponParent.Direction = getDirection();

    }
    

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

        // Update Animator for sprite direction
        if (movementInput != Vector2.zero && !inMenu)
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
        //Checking the stamina amount if the player has enough for another dodge.
        //If the player is in a dodge or attacking at the moment don't let it dodge.
        if(playerData.isEnoughStamina(25f) && CurrentState != PlayerStates.DODGE && !weaponParent.IsAttacking && !inMenu)
        {
            CurrentState = PlayerStates.DODGE;
            playerData.UseStamina(25f);
        }
    }

    void OnDodgeFinished()
    {
        //Unlock the satelock and checking for input to decide which animation should play.
        stateLock = false;
        if(movementInput != Vector2.zero)
            CurrentState = PlayerStates.WALK;
        else
            CurrentState = PlayerStates.IDLE;

        dodgeSpeed = 10f;
    }


    void OnFire()
    {
        //Checking the stamina amount if the player has enough for another attack.
        //If the player is in a dodge or attacking at the moment don't let it attack.
        if (CurrentState != PlayerStates.DODGE && !weaponParent.IsAttacking && !inMenu && !HoveringMenu)
        {
            currentWeapon.GetComponent<WeaponData>().Attack?.Invoke();
        }
    }
    void OnWeaponChange() 
    {
        if (!inMenu && CurrentState != PlayerStates.DODGE && !weaponParent.IsAttacking)
        {
            //change weapon to key R
            weapons[currentWeaponIndex].SetActive(false);
            currentWeaponIndex++;
            if (currentWeaponIndex >= weapons.Count)
            {
                currentWeaponIndex = 0;
            }
            weapons[currentWeaponIndex].SetActive(true);
            currentWeapon = weapons[currentWeaponIndex];

            //change renderers
            weaponParent.ChangeWeapon(currentWeapon);
        }
    }

    Vector2 getDirection() 
    {
        return LastMoveDir;
    }

    void OnHeal()
    {
        //obsolete for now
    }

    void OnInventory(){
        if(inventoryOpen){
            CloseInventory?.Invoke();
            inventoryOpen = false;
        } else {
            OpenInventory?.Invoke();
            inventoryOpen = true;
        }
    }
    

    public void InMenu(bool b)
    {
        inMenu = b;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
            
        }
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
    
    public int GetDamage()
    {
        return weapons[currentWeaponIndex].GetComponent<WeaponData>().GetDamage();
    }
    
    public bool Pay(int c)
    {
        return inventory.RemoveItemType(Item.ItemType.COIN, c);
    }
}